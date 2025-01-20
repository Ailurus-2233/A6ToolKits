using A6ToolKits.Attributes;
using A6ToolKits.Database.Managers;

namespace A6ToolKits.Database.Configs;

/// <inheritdoc />
[ConfigName("CSVFile")]
public class CSVConfigItem : DatabaseConfigItemBase
{
    /// <inheritdoc />
    public override bool IsNecessary => false;
    
    /// <summary>
    ///     数据库名称
    /// </summary>
    public override string Name { get; set; } = "database_csv";

    /// <inheritdoc />
    public override ManagerBase GenerateManager()
    {
        return new CsvDatabaseManager(Path, Name);
    }

    /// <summary>
    ///     数据库文件路径
    /// </summary>
    public string Path { get; set; } = "data";

    /// <inheritdoc />
    public override void SetDefault()
    {
        Name = "database_csv";
        Path = "data";
    }
}