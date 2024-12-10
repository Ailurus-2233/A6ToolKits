using A6ToolKits.Database.Attributes;
using A6ToolKits.Database.DataModels;
using A6ToolKits.Database.Enums;

namespace ToDoList.Models.RelationModels;

[TableName("task_tag_relation")]
public class TaskTagRelation : DataModelBase
{
    [PrimaryKey]
    [ColumnType("task_id", ColumnType.SQLITE_INTEGER)]
    public int TaskId { get; set; } = 0;
    
    [PrimaryKey]
    [ColumnType("tag_id", ColumnType.SQLITE_INTEGER)]
    public int TagId { get; set; } = 0;
}