using System.Linq;
using System.Reflection;
using A6Application.Views;
using A6ToolKits.Bootstrapper;
using A6ToolKits.Layout;
using A6ToolKits.MVVM.Common;
using Avalonia.Controls;

namespace A6Application;

public class AppBootstrapper : BaseBootstrapper<App, Window>
{
    public override void OnCompleted()
    {
        if (Modules?.FirstOrDefault(m => m is LayoutModule) is LayoutModule layoutModule)
            MainWindow = layoutModule.WindowLayout?.WindowContainer;
        base.OnCompleted();
    }
}