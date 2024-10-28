using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Styling;

namespace A6ToolKits.Bootstrapper.Interfaces;

/// <summary>
///     一些 Avalonia 应用控制的接口
/// </summary>
public interface IApplicationController
{
    ClassicDesktopStyleApplicationLifetime? ApplicationLifetime { get; set; }
    string[]? RunArguments { get; set; }
    AppBuilder? AppBuilder { get; set; }

    public void StopApplication();
    
    /// <summary>
    ///     应用的启动方法
    /// </summary>
    public void Run(string[] args);
    
    ThemeVariant Theme { get; set; }
    
    Window? Window { set; get; }
}