using A6ToolKits.Common.Container;
using A6ToolKits.Container.Attributes;
using A6ToolKits.Database;
using A6ToolKits.Services;
using ToDoList.Models;

namespace ToDoList.Services;

[AutoRegister(typeof(TagManagerService), RegisterType.Singleton)]
public class TagManagerService : IService
{
    private IManager? _dataManager => IoC.GetInstance<IDatabaseModule>()?.GetDatabaseManger("Data");
    public List<TaskTag> TagList { get; } = [];
    
    public void Initialize()
    {
        TagList.Clear();
        // Load tags from database
        var tags = _dataManager?.TryLoad<TaskTag>();
        if (tags == null)
            return;
        TagList.AddRange(tags);
    }

    public void OnExit()
    {
        _dataManager?.Save(TagList);
    }

    public bool CheckTagExist(string name)
    {
        return TagList.Any(tag => tag.Name == name);
    }
    
    public TaskTag? GetTag(string name)
    {
        return TagList.FirstOrDefault(tag => tag.Name == name);
    }
    
    public void AddTag(TaskTag tag)
    {
        if (TagList.Contains(tag))
            return;
        TagList.Add(tag);
    }
}