using A6ToolKits.Database.Attributes;
using A6ToolKits.Database.DataModels;
using A6ToolKits.Database.Enums;

namespace DatabaseModuleTest.DataModels;

[TableName(nameof(SQLTestModel))]
public class SQLTestModel: DataModelBase
{
    [PrimaryKey]
    [ColumnType("Id", ColumnType.SQLITE_INTEGER)]
    public int Id { get; set; } = 0;

    [ColumnType("name", ColumnType.SQLITE_TEXT)]
    public string Name { get; set; } = string.Empty;

    [ColumnType("age", ColumnType.SQLITE_NUMERIC)]
    public int Age { get; set; } = 0;
}