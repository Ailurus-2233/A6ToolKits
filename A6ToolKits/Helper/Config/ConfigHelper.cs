using System;
using System.IO;
using System.Xml;
using Serilog;

namespace A6ToolKits.Helper.Config;

public static class ConfigHelper
{
    private static readonly string ConfigPath = "config.xml";
    private static readonly string DefaultConfig = "default_config.xml";

    public static XmlNodeList? GetElements(string elementName)
    {
        try
        {
            if (!File.Exists(ConfigPath)) File.Copy(DefaultConfig, ConfigPath);

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
}