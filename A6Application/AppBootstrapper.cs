using System.Linq;
using A6ToolKits.Bootstrapper;
using A6ToolKits.Layout;
using A6ToolKits.Module;
using Avalonia.Controls;

namespace A6Application;

public class AppBootstrapper : BaseBootstrapper<App, Window>
{
    public override void OnCompleted()
    {
        if (ModuleLoader.TryGetModule<LayoutModule>(out var layoutModule))
        {
            MainWindow = layoutModule?.WindowLayout?.WindowContainer;
        }
        // MainWindow = IoC.Get<MainWindow>();
        base.OnCompleted();
    }
}