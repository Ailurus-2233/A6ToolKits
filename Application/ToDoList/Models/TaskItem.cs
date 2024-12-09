using A6ToolKits.Common.Container;
using A6ToolKits.Database;
using A6ToolKits.Database.Attributes;
using A6ToolKits.Database.DataModels;
using A6ToolKits.Database.Enums;

namespace ToDoList.Models;

[TableName("task_item")]
public class TaskItem : DataModelBase,ITask
{
    [PrimaryKey]
    [AutoIncrement]
    [ColumnType("id", ColumnType.SQLITE_INTEGER)]
    public int Id { get; set; } = 0;
    
    [ColumnType("name", ColumnType.SQLITE_TEXT)]
    public string Name { get; set; } = "";
    
    [ColumnType("content", ColumnType.SQLITE_TEXT)]
    public string Content { get; set; } = "";
    
    [ColumnType("percent_complete", ColumnType.SQLITE_INTEGER)] 
    public DateTime StartTime { get; set; }
    
    [ColumnType("due_time", ColumnType.SQLITE_INTEGER)] 
    public DateTime DueTime { get; set; }
    
    [ColumnType("finish_time", ColumnType.SQLITE_INTEGER)]
    public DateTime FinishTime { get; set; }
    
    [ColumnType("parent_id", ColumnType.SQLITE_INTEGER)]
    public int ParentId { get; set; } = 0;
    
    [ColumnType("percent_complete", ColumnType.SQLITE_INTEGER)]
    public int PercentComplete { get; set; } = 0;
    
    public List<ITask> SubTasks { get; set; } = [];
    public List<TaskTag> Tags { get; set; } = [];
    
    public void AddTag(TaskTag tag)
    {
        if (Tags.Contains(tag))
            return;
        Tags.Add(tag);
    }

    public void RemoveTag(TaskTag tag)
    {
        if (!Tags.Contains(tag))
            return;
        Tags.Remove(tag);
    }

    public void LoadTags()
    {
        var manager = IoC.GetInstance<IDatabaseModule>()?.GetDatabaseManger("Task");
        if (manager == null)
            return;
        var relations = manager.Select<TaskTagRelation>(t => t.TaskId == Id);
        foreach (var relation in relations)
        {
            var tag = manager.Select<TaskTag>(t => t.Id == relation.TagId).FirstOrDefault();
            if (tag != null)
                Tags.Add(tag);
        }
    }

    public void AddSubTask(ITask task)
    {
        if (SubTasks.Contains(task))
            return;
        SubTasks.Add(task);
    }

    public void RemoveSubTask(ITask task)
    {
        if (!SubTasks.Contains(task))
            return;
        SubTasks.Remove(task);
    }

    public void AddComment(string comment)
    {
        // Add comment to database
    }
}