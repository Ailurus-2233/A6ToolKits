using A6ToolKits.Database.Attributes;
using A6ToolKits.Database.DataModels;
using A6ToolKits.Database.Enums;

namespace ToDoList.Models;

[TableName("task_tag")]
public class TaskTag : DataModelBase
{
    [PrimaryKey]
    [AutoIncrement]
    [ColumnType("id", ColumnType.SQLITE_INTEGER)]
    public int Id { get; set; } = 0;
    
    [ColumnType("name", ColumnType.SQLITE_TEXT)]
    public string Name { get; set; } = "";
    
    [ColumnType("color", ColumnType.SQLITE_TEXT)]
    public string Color { get; set; } = "";
    
    [ColumnType("description", ColumnType.SQLITE_TEXT)]
    public string Description { get; set; } = "";
}