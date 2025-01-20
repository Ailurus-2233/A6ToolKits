using A6ToolKits.Container;
using A6ToolKits.EventAggregator;
using A6ToolKits.Exceptions;
using A6ToolKits.Starter.Events;

namespace A6ToolKits.Modules;

public static class ModuleLoader
{
    private static List<IModule> GetModuleList()
    {
        throw new NotImplementedException();
    }
    
    public static void LoadModules()
    {
        var moduleList = GetModuleList();
        var eventAggregator = IoC.GetInstance<IEventAggregator>();
        if (moduleList is not { Count: > 0 }) return;
        
        moduleList.ForEach(module =>
        {
            try
            {
                var target = IoC.GetInstance(module.GetType()) as IModule;
                target?.OnLoadModule();
                eventAggregator?.Subscribe<ApplicationExitEvent>(e => target?.OnUnloadModule());
            }
            catch (Exception e)
            {
                throw new LoadModuleException(module.GetType().Name, e.StackTrace);
            }
        });
    }
}