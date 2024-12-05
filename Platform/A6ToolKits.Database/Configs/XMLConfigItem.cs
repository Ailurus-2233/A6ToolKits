using System.Xml;
using A6ToolKits.Configuration;
using A6ToolKits.Configuration.Attributes;

namespace A6ToolKits.Database.Configs;

/// <summary>
///     XML 数据库配置
/// </summary>
[ConfigName("XMLFile")]
public class XMLConfigItem : ConfigItemBase
{
    /// <inheritdoc />
    public override bool IsNecessary { get; } = false;

    /// <inheritdoc />
    public override void SetDefault()
    {
        
    }

    /// <inheritdoc />
    public override void OnLoadedConfig()
    {
        
    }
}