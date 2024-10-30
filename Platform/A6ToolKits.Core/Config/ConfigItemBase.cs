using System;
using System.Xml;

namespace A6ToolKits.Config;

/// <summary>
///     配置项基类，利用反射可以从 XmlNode 生成配置项
/// </summary>
public abstract class ConfigItemBase
{
    /// <summary>
    ///     从 XmlNode 生成配置项
    /// </summary>
    /// <param name="node">
    ///     XmlNode
    /// </param>
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