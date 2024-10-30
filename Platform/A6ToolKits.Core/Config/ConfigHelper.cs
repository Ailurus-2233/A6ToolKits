using System;
using System.IO;
using System.Xml;
using A6ToolKits.Exceptions;
using Serilog;

namespace A6ToolKits.Config;

/// <summary>
///     配置帮助类，配置文件名称为 config.xml，如果配置文件不存在则从
///     default_config.xml 复制
/// </summary>
public static class ConfigHelper
{
    private const string ConfigPath = "config.xml";
    private const string DefaultConfig = "default_config.xml";

    /// <summary>
    ///     获取配置项
    /// </summary>
    /// <param name="elementName">
    ///     配置项名称
    /// </param>
    /// <returns>
    ///     返回配置项
    /// </returns>
    /// <exception cref="ConfigLoadException">
    ///     加载配置文件失败
    /// </exception>
    public static XmlNodeList? GetElements(string elementName)
    {
        try
        {
            if (!File.Exists(ConfigPath))
            {
                if (!File.Exists(DefaultConfig))
                {
                    GenerateDefaultConfigFile();
                }

                File.Copy(DefaultConfig, ConfigPath);
            }

            var xml = new XmlDocument();
            xml.Load(ConfigPath);
            var root = xml.DocumentElement;
            return root?.GetElementsByTagName(elementName);
        }
        catch (Exception e)
        {
            Log.Error("Failed to load configuration file: {0}", e.Message);
            throw new ConfigLoadException(e.Message);
        }
    }


    private static void GenerateDefaultConfigFile()
    {
        var doc = new XmlDocument();
        var root = doc.CreateElement("Configuration");

        var libraries = doc.CreateElement("Libraries");
        var path = doc.CreateElement("Path");
        path.SetAttribute("Path", "./");
        libraries.AppendChild(path);
        root.AppendChild(libraries);

        var layout = doc.CreateElement("Layout");
        layout.SetAttribute("Assembly", "");
        layout.SetAttribute("Target", "");
        root.AppendChild(layout);

        doc.AppendChild(root);
        doc.Save(DefaultConfig);
    }
}