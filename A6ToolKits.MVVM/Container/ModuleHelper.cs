namespace A6ToolKits.MVVM.Container;

public static class ModuleHelper
{
    private static IModuleCatalog? _moduleCatalog;

    public static void SetModuleCatalog(IModuleCatalog moduleCatalog)
    {
        _moduleCatalog = moduleCatalog;
    }

    public static void LoadModule<TModule>() where TModule : IModule, new()
    {
        _moduleCatalog?.AddModule<TModule>();
    }
    
    public static void LoadModule(Type moduleType, InitializationMode mode = InitializationMode.WhenAvailable)
    {
        _moduleCatalog?.AddModule(new ModuleInfo()
        {
            ModuleName = moduleType.Name,
            ModuleType = moduleType.AssemblyQualifiedName,
            InitializationMode = mode
        });
    }
    
}