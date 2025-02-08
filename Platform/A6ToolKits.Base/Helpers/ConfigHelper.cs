using System.Reflection;
using System.Xml;
using A6ToolKits.Attributes;
using A6Toolkits.Configuration;

namespace A6ToolKits.Helpers;

/// <summary>
///     配置帮助类，配置文件名称为 config.xml，如果配置文件不存在则从 default_config.xml 复制
///     主要功能有：
///         1. 从配置文件获取指定名称配置项的 XmlElement，首先从 config.xml 获取，如果不存在则从 default_config.xml 获取
///         2. 根据属性 ConfigItemAttribute 生成默认配置文件，生成的配置文件为 default_config.xml
///         3. 拷贝默认配置文件到配置文件
/// </summary>
public static class ConfigHelper
{
    private const string ConfigPath = "config.xml";
    private const string DefaultConfig = "default_config.xml";

    /// <summary>
    ///     从配置文件获取指定名称配置项的 XmlElement，首先从 config.xml 获取，如果不存在则从 default_config.xml 获取
    /// </summary>
    /// <param name="config">
    ///     配置项
    /// </param>
    /// <param name="elementName">
    ///     配置项名称
    /// </param>
    /// <returns>
    ///     返回配置项
    /// </returns>
    public static void LoadConfigFromFile(this ConfigItemBase config)
    {
        var configDoc = new XmlDocument();
        var defaultConfigDoc = new XmlDocument();
        
        // 加载配置文件
        if (File.Exists(ConfigPath))
            configDoc.Load(ConfigPath);
        
        if (File.Exists(DefaultConfig))
            defaultConfigDoc.Load(DefaultConfig);
        
        var root = configDoc.DocumentElement;
        var result = root?.GetElementsByTagName(elementName);
        
        var defaultRoot = defaultConfigDoc.DocumentElement;
        var defaultResult = defaultRoot?.GetElementsByTagName(elementName);
        
        return result?.Count > 0 ? result : defaultResult;
    }


    /// <summary>
    ///     生成默认配置文件
    /// </summary>
    public static void GenerateDefaultConfigFile()
    {
        var doc = new XmlDocument();
        var root = doc.CreateElement("Configuration");
        doc.AppendChild(root);

        var assemblies = AssemblyHelper.GetAllAssemblies();

        var configItems = new List<Type>();

        foreach (var assembly in assemblies.Select(Assembly.Load))
        {
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.GetCustomAttribute<ModuleConfigAttribute>() != null)
                    configItems.Add(type);
            }
        }

        if (configItems.Count != 0)
        {
            foreach (var item in configItems)
            {
                if (Activator.CreateInstance(item) is not IConfigItem config)
                    continue;
                var node = config.CreateDefaultConfig(doc);
                root.AppendChild(node);
            }
        }

        doc.Save(DefaultConfig);
    }

    /// <summary>
    ///     添加配置项到默认配置文件
    /// </summary>
    /// <param name="node">
    ///     配置项节点
    /// </param>
    public static void AddXmlNodeToDefaultConfigFile(XmlElement node)
    {
        Document.Load(DefaultConfig);
        var root = Document.DocumentElement;
        if (root == null) return;
        var oldNode = root.GetElementsByTagName(node.Name);
        if (oldNode.Count > 0)
            foreach (XmlElement o in oldNode)
                root.RemoveChild(o);
        root.AppendChild(node);
        Document.Save(DefaultConfig);
    }

    /// <summary>
    ///     设置配置节点
    /// </summary>
    /// <param name="nodeName">
    ///     节点名称
    /// </param>
    /// <param name="node">
    ///     节点内容
    /// </param>
    public static void SetConfigNode(string nodeName, XmlElement node)
    {
        Document.Load(ConfigPath);
        var root = Document.DocumentElement;
        if (root == null) return;
        var oldNode = root.GetElementsByTagName(nodeName);

        if (oldNode.Count > 0)
        {
            var copyNode = new XmlElement?[oldNode.Count];
            for (var i = 0; i < oldNode.Count; i++)
                copyNode[i] = (XmlElement?)oldNode[i];

            foreach (var o in copyNode)
            {
                if (o != null)
                    root.RemoveChild(o);
            }
        }

        root.AppendChild(node);
        Document.Save(ConfigPath);
    }

    /// <summary>
    ///     获取配置文档
    /// </summary>
    /// <returns>
    ///     返回配置文档
    /// </returns>
    public static XmlDocument GetConfigDocument()
    {
        Document.Load(ConfigPath);
        return Document;
    }
}