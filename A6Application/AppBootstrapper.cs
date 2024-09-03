using System.Reflection;
using A6Application.Views;
using A6ToolKits.Bootstrapper;
using A6ToolKits.MVVM.Common;
using A6ToolKits.MVVM.Utils;

namespace A6Application;

public class AppBootstrapper : BaseBootstrapper<App, MainWindow>
{
    public override void OnCompleted()
    {
        var assembly = Assembly.GetExecutingAssembly();
        IoCHelper.AutoRegister(assembly);
        MainWindow = IoC.Get<MainWindow>();

        base.OnCompleted();
    }
}