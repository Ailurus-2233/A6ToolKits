using System.Collections.Generic;
using System.Diagnostics;
using A6ToolKits.Bootstrapper.Extensions;
using A6ToolKits.Helper;
using A6ToolKits.Module;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Serilog;
using LogEventLevel = Serilog.Events.LogEventLevel;

namespace A6ToolKits.Bootstrapper;

public class BaseBootstrapper<TApplication, TWindow> : IBootstrapper
    where TApplication : Application, new()
    where TWindow : Window, new()
{
    private AppBuilder? _builder;
    private ClassicDesktopStyleApplicationLifetime? _lifetime;
    private string[] _runArgs = [];
    protected TWindow? MainWindow { get; set; }
    public IList<ModuleBase>? Modules { get; set; }


    public virtual void Initialize()
    {
        LoggerHelper.InitializeConsoleLogger(LogEventLevel.Verbose);
        Log.Information($"Initializing Application: {MainWindow?.Title ?? "A6ToolKits Application"}");

        Log.Information("Finish Initializing Application.");
    }

    public virtual void Configure()
    {
        Log.Information("Configuring Application: Config Avalonia builder & lifetime");

        // 如果是 Debug 模式，则启用Avalonia的日志记录
        if (Debugger.IsAttached)
            _builder = AppBuilder
                .Configure<TApplication>()
                .UsePlatformDetect()
                .LogToTrace();
        else
            _builder = AppBuilder
                .Configure<TApplication>()
                .UsePlatformDetect();

        _lifetime = LifetimeExtensions.PrepareLifetime(_builder, _runArgs, null);
        _builder.SetupWithLifetime(_lifetime);

        // 加载模块
        Modules = ModuleLoader.LoadModules();
    }

    public virtual void OnCompleted()
    {
        if (_lifetime == null) return;
        MainWindow ??= new TWindow();
        _lifetime.MainWindow = MainWindow;

        Log.Information("Finish Configuring Application.");

        Log.Information("Starting Application...");
        _lifetime.Start(_runArgs);
    }


    public virtual void Run(string[] args)
    {
        _runArgs = args;
        Initialize();
        Configure();
        OnCompleted();
        Log.Information("Application Stopped.");
    }
}