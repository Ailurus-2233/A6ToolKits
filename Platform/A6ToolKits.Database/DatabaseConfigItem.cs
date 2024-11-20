using System.Xml;
using A6ToolKits.Configuration;

namespace A6ToolKits.Database;

/// <summary>
///     数据库配置项
/// </summary>
public class DatabaseConfigItem: IConfigItem
{
    /// <inheritdoc />
    public void LoadConfig()
    {
        
    }

    /// <inheritdoc />
    public XmlElement CreateDefaultConfig(string tagName)
    {
        throw new NotImplementedException();
    }
    
    /// <inheritdoc />
    public void SetDefault()
    {
        throw new NotImplementedException();
    }
}