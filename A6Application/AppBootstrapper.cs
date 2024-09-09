using System.Linq;
using A6ToolKits.Bootstrapper;
using A6ToolKits.Layout;
using Avalonia.Controls;

namespace A6Application;

public class AppBootstrapper : BaseBootstrapper<App, Window>
{
    public override void OnCompleted()
    {
        if ((bool)Modules?.Any(m => m is LayoutModule))
            MainWindow = LayoutModule.WindowLayout?.WindowContainer;

        // MainWindow = IoC.Get<MainWindow>();
        base.OnCompleted();
    }
}