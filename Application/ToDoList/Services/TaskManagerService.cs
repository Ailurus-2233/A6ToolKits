using A6ToolKits.Attributes;
using A6ToolKits.Services;

namespace ToDoList.Services;

[AutoRegister(typeof(TaskManagerService), RegisterType.Singleton)]
public class TaskManagerService : ServiceBase
{
    
    
    public override void Initialize()
    {
        throw new NotImplementedException();
    }

    public override void OnExit()
    {
        throw new NotImplementedException();
    }
}