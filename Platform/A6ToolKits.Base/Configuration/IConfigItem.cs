using System.Xml;

namespace A6ToolKits.Configuration;

public interface IConfigItem
{
    void LoadConfig();
    XmlElement CreateDefaultConfig();
    void SetDefault();
}