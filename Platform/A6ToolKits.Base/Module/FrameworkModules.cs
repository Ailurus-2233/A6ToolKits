using A6ToolKits.Modules;

namespace A6ToolKits.Module;

public interface ILayoutModule : IModule
{
    void SetMainWindow();
}

public interface IUIPackageModule : IModule;