using System.Reflection;
using System.Xml;
using A6ToolKits.Configuration.Attributes;
using A6ToolKits.Configuration.Exceptions;

namespace A6ToolKits.Configuration;

/// <summary>
///     配置项基类，利用反射可以从 XmlNode 生成配置项
/// </summary>
public abstract class ConfigItemBase : IConfigItem
{
    /// <inheritdoc />
    public List<IConfigItem> Children { get; } = [];

    /// <summary>
    ///     配置项是否加载完成
    /// </summary>
    public bool LoadedFinished { get; set; } = false;

    /// <summary>
    ///     配置项是否需要
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
    protected string GetNodeName()
    {
        var nodeName = GetType().GetCustomAttribute<ConfigNameAttribute>()?.Name;
        if (nodeName == null)
            throw new ConfigLoadException(GetType(),
                "You must set the ConfigNameAttribute for the config item.");
        return nodeName;
    }

    /// <inheritdoc />
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
            GenerateFromXmlNode(configNode);
            LoadedFinished = true;
            OnLoadedConfig();
        }
    }

    /// <inheritdoc />
    public abstract void SetDefault();

    /// <inheritdoc />
    public virtual void OnLoadedConfig()
    {
    }

    private static List<string> _skipProperties =>
    [
        nameof(Children),
        nameof(LoadedFinished),
        nameof(IsNecessary)
    ];

    /// <inheritdoc />
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
            if (_skipProperties.Contains(name))
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
    protected void GenerateFromXmlNode(XmlNode node)
    {
        var property = GetType().GetProperties();
        SetDefault();
        foreach (var prop in property)
        {
            var name = prop.Name;
            if (_skipProperties.Contains(name))
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
            child.LoadedFinished = true;
            child.OnLoadedConfig();
        }
    }


    /// <inheritdoc />
    public XmlElement GenerateXmlNode(XmlDocument doc)
    {
        var result = doc.CreateElement(GetNodeName());
        var property = GetType().GetProperties();
        foreach (var prop in property)
        {
            var name = prop.Name;
            if (_skipProperties.Contains(name))
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

    private ConfigItemBase CreateItem(string nodeName)
    {
        var type = GetType().Assembly.GetTypes()
            .Where(t => t.GetCustomAttribute<ConfigNameAttribute>()?.Name == nodeName).ToList();
        if (type == null || type.Count == 0)
            throw new ConfigLoadException(GetType(), $"Can't find the config item {nodeName}");

        if (type.Count > 1)
            throw new ConfigLoadException(GetType(), $"Find more than one config item {nodeName}");

        var instance = Activator.CreateInstance(type[0]) as ConfigItemBase;

        if (instance == null)
            throw new ConfigLoadException(GetType(), $"Can't create the config item {nodeName}");

        return instance;
    }

    /// <inheritdoc />
    public void SaveConfig()
    {
        var doc = ConfigHelper.GetConfigDocument();
        var nodeName = GetNodeName();
        var node = GenerateXmlNode(doc);
        ConfigHelper.SetConfigNode(nodeName, node);
    }
}