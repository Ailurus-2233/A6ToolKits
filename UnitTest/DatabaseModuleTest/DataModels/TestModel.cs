using A6ToolKits.Database;
using A6ToolKits.Database.Attributes;
using A6ToolKits.Database.DataModels;
using A6ToolKits.Database.Enums;

namespace DatabaseModuleTest.DataModels;

[TableName(nameof(TestModel))]
public class TestModel : DataModelBase
{
    [PrimaryKey]
    [ColumnType("Id", ColumnType.FILE_INTEGER)]
    public int Id { get; set; } = 0;

    [ColumnType("name", ColumnType.FILE_STRING)]
    public string Name { get; set; } = string.Empty;

    [ColumnType("age", ColumnType.FILE_INTEGER)]
    public int Age { get; set; } = 0;
}