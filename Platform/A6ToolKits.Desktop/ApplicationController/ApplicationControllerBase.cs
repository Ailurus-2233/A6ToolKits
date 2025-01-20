using System.Diagnostics;
using A6ToolKits.LifetimeExtension;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;

namespace A6ToolKits.ApplicationController;

public abstract class ApplicationControllerBase<TApplication, TWindow> : IApplicationController
    where TApplication : Application, new()
    where TWindow : Window, new()
{
    protected AppBuilder? _appBuilder;
    protected ClassicDesktopStyleApplicationLifetime? _lifetime;
    protected string[]? _runArguments;

    public Window MainWindow { get; set; } = new TWindow();
    public ThemeVariant Theme { get; set; } = ThemeVariant.Default;

    public virtual void Run(string[] args)
    {
        _runArguments = args;
        Initialize();
    }

    public virtual void StopApplication()
    {
        _lifetime?.Shutdown();
    }

    private void Initialize()
    {
        // 如果是 Debug 模式，则启用Avalonia的日志记录
        if (Debugger.IsAttached)
            _appBuilder = AppBuilder
                .Configure<TApplication>()
                .UsePlatformDetect()
                .LogToTrace();
        else
            _appBuilder = AppBuilder
                .Configure<TApplication>()
                .UsePlatformDetect();

        _lifetime = DesktopLifetimeExtension.PrepareLifetime(_appBuilder, _runArguments ?? [], null);
        _appBuilder.SetupWithLifetime(_lifetime);
    }
}