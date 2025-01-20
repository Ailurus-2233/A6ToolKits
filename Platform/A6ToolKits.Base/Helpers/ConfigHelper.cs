using System.Reflection;
using System.Xml;
using A6ToolKits.Attributes;
using A6Toolkits.Configuration;
using SysAssembly = System.Reflection.Assembly;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace A6ToolKits.Helpers;

/// <summary>
///     配置帮助类，配置文件名称为 config.xml，如果配置文件不存在则从
///     default_config.xml 复制
/// </summary>
public static class ConfigHelper
{
    private const string ConfigPath = "config.xml";
    private const string DefaultConfig = "default_config.xml";
    private static readonly XmlDocument Document = new();

    /// <summary>
    ///     获取配置项
    /// </summary>
    /// <param name="elementName">
    ///     配置项名称
    /// </param>
    /// <returns>
    ///     返回配置项
    /// </returns>
    public static XmlNodeList? GetElements(string elementName)
    {
        // 如果配置文件不存在则从默认配置文件复制
        if (!File.Exists(ConfigPath))
        {
            if (!File.Exists(DefaultConfig))
                GenerateDefaultConfigFile();
            File.Copy(DefaultConfig, ConfigPath);
        }

        // 加载配置文件
        Document.Load(ConfigPath);
        var root = Document.DocumentElement;
        return root?.GetElementsByTagName(elementName);
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

        foreach (var assembly in assemblies.Select(SysAssembly.Load))
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

    /// <summary>
    ///     获取默认配置文档
    /// </summary>
    /// <returns>
    ///     返回默认配置文档
    /// </returns>
    public static XmlDocument GetDefaultConfigDocument()
    {
        Document.Load(DefaultConfig);
        return Document;
    }
}