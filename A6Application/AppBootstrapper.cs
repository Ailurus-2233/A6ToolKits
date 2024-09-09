using System.Linq;
using A6ToolKits.Bootstrapper;
using A6ToolKits.Layout;
using Avalonia.Controls;

namespace A6Application;

public class AppBootstrapper : BaseBootstrapper<App, Window>
{
    public override void OnCompleted()
    {
        if (Modules?.FirstOrDefault(m => m is LayoutModule) is LayoutModule layoutModule)
            MainWindow = layoutModule.WindowLayout?.WindowContainer;

        // MainWindow = IoC.Get<MainWindow>();
        base.OnCompleted();
    }
}