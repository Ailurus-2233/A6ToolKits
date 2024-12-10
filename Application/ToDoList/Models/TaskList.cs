using System.ComponentModel.DataAnnotations.Schema;
using A6ToolKits.Database.Attributes;
using A6ToolKits.Database.DataModels;
using A6ToolKits.Database.Enums;

namespace ToDoList.Models;

[TableName("task_list")]
public class TaskList : DataModelBase
{
    [PrimaryKey]
    [AutoIncrement]
    [ColumnType("id", ColumnType.SQLITE_INTEGER)]
    public int Id { get; set; }

    [ColumnType("name", ColumnType.SQLITE_TEXT)]
    public string Name { get; set; } = "";

    [ColumnType("description", ColumnType.SQLITE_INTEGER)]
    public DateTime CreatedDate { get; set; }
    
    public List<Task> Tasks { get; set; } = [];
}