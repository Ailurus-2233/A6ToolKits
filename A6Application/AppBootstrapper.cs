using System.Reflection;
using A6Application.Views;
using A6ToolKits.Bootstrapper;
using A6ToolKits.MVVM.Common;

namespace A6Application;

public class AppBootstrapper : BaseBootstrapper<App, MainWindow>
{
    public override void OnCompleted()
    {
        MainWindow = IoC.Get<MainWindow>();
        base.OnCompleted();
    }
}