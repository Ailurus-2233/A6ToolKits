using A6ToolKits;
using A6ToolKits.Bootstrapper;
using A6ToolKits.Layout;
using A6ToolKits.Module;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;

namespace UIDemo;

public class Bootstrapper : BaseBootstrapper<BaseApp, Window>
{
    /// <summary>
    ///     重写完成方法，在完成时设置主窗体
    /// </summary>
    public override void OnCompleted()
    {
        MainWindow = ModuleLoader.TryGetModule<LayoutModule>(out var layoutModule)
            ? layoutModule?.WindowLayout?.DefaultWindow
            : new Window();
        var current = Application.Current;
        if (current != null) current.RequestedThemeVariant = ThemeVariant.Dark;
        base.OnCompleted();
    }
}