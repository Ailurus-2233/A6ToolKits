using System;
using System.Xml;

namespace A6ToolKits.Helper.Config;

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