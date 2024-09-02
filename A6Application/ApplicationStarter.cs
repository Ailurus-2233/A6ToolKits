using System.Reflection;
using A6Application.ViewModels;
using A6ToolKits.MVVM;
using A6ToolKits.MVVM.Common;

namespace A6Application;

public class ApplicationStarter : Starter
{
    protected override void ConfigureAssembly()
    {
        base.ConfigureAssembly();
        var assembly = Assembly.GetExecutingAssembly();
        IoCHelper.AutoRegister(assembly);
    }
}