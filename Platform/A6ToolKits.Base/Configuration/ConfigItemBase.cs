using System.Reflection;
using System.Xml;
using A6ToolKits.Attributes;
using A6ToolKits.Exceptions;
using A6ToolKits.Helpers;

namespace A6Toolkits.Configuration;

/// <summary>
///     配置项基类，利用反射可以从 XmlNode 生成配置项
/// </summary>
public abstract class ConfigItemBase : IConfigItem
{
    /// <summary>
    ///     子配置项
    /// </summary>
    public IList<IConfigItem> Children { get; } = [];

    /// <summary>
    ///     配置项是否需要:
    ///         True    必须存在，否则抛出异常
    ///         False   可选项
    /// </summary>
    public abstract bool IsNecessary { get; }


    /// <summary>
    ///     获取配置项名称
    /// </summary>
    /// <returns>
    ///     返回配置项名称
    /// </returns>
    /// <exception cref="ConfigLoadException">
    ///     当配置项名称没有设置 ConfigNameAttribute 时抛出异常
    /// </exception>
    private string GetNodeName()
    {
        var nodeName = GetType().GetCustomAttribute<ConfigNameAttribute>()?.Name;
        if (nodeName == null)
            throw new ConfigLoadException(GetType(),
                "You must set the ConfigNameAttribute for the config item.");
        return nodeName;
    }
    
    /// <summary>
    ///     从 config.xml 加载配置
    /// </summary>
    /// <exception cref="ConfigLoadException"></exception>
    public void LoadConfig()
    {
        var nodeName = GetNodeName();
        var configNode = ConfigHelper.GetElements(nodeName)?.Item(0);
        if (configNode == null)
        {
            if (IsNecessary)
                throw new ConfigLoadException(GetType(), $"{nodeName} not founded in config file.");
        }
        else
        {
            try
            {
                GenerateFromXmlNode(configNode);
            } catch (Exception)
            {
                throw new ConfigLoadException(GetType(), $"Load {nodeName} failed.");
            }
        }
    }

    /// <summary>
    ///     设置默认配置
    /// </summary>
    public abstract void SetDefault();

    /// <summary>
    ///     跳过的属性
    /// </summary>
    private static List<string> SkipProperties =>
    [
        nameof(Children),
        nameof(IsNecessary)
    ];

    /// <summary>
    ///     创建默认配置
    /// </summary>
    /// <param name="doc">
    ///     XmlDocument 对象, 用于创建 XmlElement
    /// </param>
    /// <returns>
    ///     返回创建的配置项 XmlElement
    /// </returns>
    /// <exception cref="ConfigLoadException">
    ///     当创建默认配置失败时抛出异常
    /// </exception>
    public XmlElement CreateDefaultConfig(XmlDocument doc)
    {
        var result = doc.CreateElement(GetNodeName());
        if (Activator.CreateInstance(GetType()) is not IConfigItem defaultConfig)
            throw new ConfigLoadException(GetType(), "Can't create the default config item.");

        defaultConfig.SetDefault();
        var property = defaultConfig.GetType().GetProperties();
        foreach (var prop in property)
        {
            var name = prop.Name;
            if (SkipProperties.Contains(name))
                continue;
            var value = prop.GetValue(this);
            if (value != null) result.SetAttribute(prop.Name, value.ToString());
        }

        foreach (var child in defaultConfig.Children)
        {
            var childNode = child.CreateDefaultConfig(doc);
            result.AppendChild(childNode);
        }

        return result;
    }

    /// <summary>
    ///     从 XmlNode 生成配置项
    /// </summary>
    /// <param name="node">
    ///     XmlNode
    /// </param>
    private void GenerateFromXmlNode(XmlNode node)
    {
        var property = GetType().GetProperties();
        SetDefault();
        foreach (var prop in property)
        {
            var name = prop.Name;
            if (SkipProperties.Contains(name))
                continue;
            var value = node.Attributes?[name]?.Value;
            if (value != null)
                prop.SetValue(this, Convert.ChangeType(value, prop.PropertyType));
        }

        Children.Clear();
        var childNodes = node.ChildNodes;
        foreach (XmlNode childNode in childNodes)
        {
            var child = CreateItem(childNode.Name);
            child.GenerateFromXmlNode(childNode);
            Children.Add(child);
        }
    }


    /// <summary>
    ///     生成 XmlNode， 用于保存配置
    /// </summary>
    /// <param name="doc">
    ///     XmlDocument 对象
    /// </param>
    /// <returns>
    ///     创建的配置项 XmlNode
    /// </returns>
    public XmlElement GenerateXmlNode(XmlDocument doc)
    {
        var result = doc.CreateElement(GetNodeName());
        var property = GetType().GetProperties();
        foreach (var prop in property)
        {
            var name = prop.Name;
            if (SkipProperties.Contains(name))
                continue;
            var value = prop.GetValue(this);
            if (value != null) result.SetAttribute(prop.Name, value.ToString());
        }

        foreach (var child in Children)
        {
            var childNode = child.GenerateXmlNode(doc);
            result.AppendChild(childNode);
        }

        return result;
    }

    /// <summary>
    ///     创建配置项
    /// </summary>
    /// <param name="nodeName">
    ///     配置项名称
    /// </param>
    /// <returns>
    ///     返回创建的配置项
    /// </returns>
    /// <exception cref="ConfigLoadException">
    ///     当创建配置项失败时抛出异常
    /// </exception>
    private ConfigItemBase CreateItem(string nodeName)
    {
        var type = GetType().Assembly.GetTypes()
            .Where(t => t.GetCustomAttribute<ConfigNameAttribute>()?.Name == nodeName).ToList();
        if (type == null || type.Count == 0)
            throw new ConfigLoadException(GetType(), $"Can't find the config item {nodeName}");

        if (type.Count > 1)
            throw new ConfigLoadException(GetType(), $"Find more than one config item {nodeName}");

        if (Activator.CreateInstance(type[0]) is not ConfigItemBase instance)
            throw new ConfigLoadException(GetType(), $"Can't create the config item {nodeName}");

        return instance;
    }

    /// <summary>
    ///     保存配置
    /// </summary>
    public void SaveConfig()
    {
        var doc = ConfigHelper.GetConfigDocument();
        var nodeName = GetNodeName();
        var node = GenerateXmlNode(doc);
        ConfigHelper.SetConfigNode(nodeName, node);
    }
}