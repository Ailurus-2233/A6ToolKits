using System.Xml;
using A6ToolKits.Configuration;
using A6ToolKits.Configuration.Attributes;
using A6ToolKits.Database.Managers;

namespace A6ToolKits.Database.Configs;

/// <summary>
///     XML 数据库配置
/// </summary>
[ConfigName("XMLFile")]
public class XMLConfigItem : DatabaseConfigItemBase
{
    /// <inheritdoc />
    public override string Name { get; set; } = "database_xml";

    /// <inheritdoc />
    public override IManager GenerateManager()
    {
        return new XMLDatabaseManager(Path, Name);
    }

    /// <summary>
    ///     数据库文件路径
    /// </summary>
    public string Path { get; set; } = "data";
    
    /// <inheritdoc />
    public override bool IsNecessary { get; } = false;

    /// <inheritdoc />
    public override void SetDefault()
    {
        Name = "database_xml";
        Path = "data";
    }
}