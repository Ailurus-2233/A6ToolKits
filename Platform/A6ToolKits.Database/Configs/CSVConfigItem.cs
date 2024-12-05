using A6ToolKits.Configuration;
using A6ToolKits.Configuration.Attributes;

namespace A6ToolKits.Database.Configs;

/// <inheritdoc />
[ConfigName("CSVFile")]
public class CSVConfigItem : ConfigItemBase
{
    /// <inheritdoc />
    public override bool IsNecessary => false;

    /// <inheritdoc />
    public override void SetDefault()
    {
        
    }

    /// <inheritdoc />
    public override void OnLoadedConfig()
    {
        
    }
}