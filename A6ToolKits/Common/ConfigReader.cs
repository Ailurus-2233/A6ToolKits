using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Serilog;

namespace A6ToolKits.Common;

public static class ConfigReader
{
    private static readonly string ConfigPath = "config.xml";
    private static readonly string DefaultConfig = "default_config.xml";
    
    public static XmlNodeList? GetElements(string elementName)
    {
        try
        {
            if (!File.Exists(ConfigPath))
            {
                File.Copy(DefaultConfig, ConfigPath);
            }
            
            var xml = new XmlDocument();
            xml.Load(ConfigPath);
            var root = xml.DocumentElement;
            return root?.GetElementsByTagName(elementName);
        } catch (Exception e)
        {
            Log.Error("Failed to load configuration file: {0}", e.Message);
            return null;
        }
        
        
    }
}