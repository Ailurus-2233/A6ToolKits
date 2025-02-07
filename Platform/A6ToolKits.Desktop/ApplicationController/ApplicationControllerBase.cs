using System.Diagnostics;
using A6ToolKits.LifetimeExtension;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Styling;
namespace A6ToolKits.ApplicationController;

public abstract class ApplicationControllerBase<TApplication, TWindow> : IApplicationController
    where TApplication : Application, new()
    where TWindow : Window, new()
{
    protected AppBuilder? AppBuilder;
    protected ClassicDesktopStyleApplicationLifetime? Lifetime;
    protected string[]? RunArguments;

    public Window? MainWindow { get; set; }
    public ThemeVariant Theme { get; set; } = ThemeVariant.Default;

    public virtual void Run(string[] args)
    {
        RunArguments = args;
        Initialize();
    }

    public virtual void StopApplication()
    {
        Lifetime?.Shutdown();
    }

    private void Initialize()
    {
        // 如果是 Debug 模式，则启用Avalonia的日志记录
        if (Debugger.IsAttached)
            AppBuilder = AppBuilder
                .Configure<TApplication>()
                .UsePlatformDetect()
                .LogToTrace();
        else
            AppBuilder = AppBuilder
                .Configure<TApplication>()
                .UsePlatformDetect();

        Lifetime = DesktopLifetimeExtension.PrepareLifetime(AppBuilder, RunArguments ?? [], null);
        AppBuilder.SetupWithLifetime(Lifetime);
    }
}