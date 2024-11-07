using System.IO;
using System.Xml;
using A6ToolKits.Common.Exceptions;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace A6ToolKits.Helper.Configurator;

/// <summary>
///     配置帮助类，配置文件名称为 config.xml，如果配置文件不存在则从
///     default_config.xml 复制
/// </summary>
public static class ConfigHelper
{
    private const string ConfigPath = "config.xml";
    private const string DefaultConfig = "default_config.xml";
    private static readonly XmlDocument document = new();
    
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
        if (!File.Exists(ConfigPath))
        {
            if (!File.Exists(DefaultConfig))
                GenerateDefaultConfigFile();
            File.Copy(DefaultConfig, ConfigPath);
        }
        
        document.Load(ConfigPath);
        var root = document.DocumentElement;
        return root?.GetElementsByTagName(elementName);
    }


    private static void GenerateDefaultConfigFile()
    {
        var doc = new XmlDocument();
        var root = doc.CreateElement("Configuration");
        doc.AppendChild(root);
        doc.Save(DefaultConfig);
    }

    public static void AddXmlNodeToDefaultConfigFile(XmlElement node)
    {
        document.Load(DefaultConfig);
        var root = document.DocumentElement;
        if (root == null) return;
        var oldNode = root.GetElementsByTagName(node.Name);
        if (oldNode.Count > 0)
        {
            foreach (XmlElement o in oldNode)
            {
                root.RemoveChild(o);
            }
        }
        root.AppendChild(node);
        document.Save(DefaultConfig);
    }
    
    public static XmlDocument GetDefaultConfig()
    {
        return document;
    }
}