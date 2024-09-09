using System;
using System.IO;
using System.Xml;
using A6ToolKits.Helper.Exceptions;
using Serilog;

namespace A6ToolKits.Helper;

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

public abstract class ConfigItemBase
{
    public void GenerateFromXmlNode(XmlNode node)
    {
        var property = GetType().GetProperties();
        foreach (var prop in property)
        {
            var value = node.Attributes?[prop.Name]?.Value;
            if (value != null) prop.SetValue(this, Convert.ChangeType(value, prop.PropertyType));
        }
    }
}