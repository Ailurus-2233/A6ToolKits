using A6ToolKits.Database.Attributes;
using A6ToolKits.Database.DataModels;
using A6ToolKits.Database.Enums;

namespace ToDoList.Models.RelationModels;

[TableName("task_list_relation")]
public class TaskListRelation: DataModelBase        
{
    [PrimaryKey]
    [ColumnType("task_id", ColumnType.SQLITE_INTEGER)]
    public int TaskId { get; set; } = 0;
    
    [PrimaryKey]
    [ColumnType("list_id", ColumnType.SQLITE_INTEGER)]
    public int ListId { get; set; } = 0;
}