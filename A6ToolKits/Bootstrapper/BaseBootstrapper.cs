using System.Collections;
using System.Collections.Generic;
using A6ToolKits.Bootstrapper.Extensions;
using A6ToolKits.Bootstrapper.Interfaces;
using A6ToolKits.Module;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Logging;
using Serilog;
using LogEventLevel = Serilog.Events.LogEventLevel;

namespace A6ToolKits.Bootstrapper;

public class BaseBootstrapper<TApplication, TWindow> : IBootstrapper
    where TApplication : Application, new()
    where TWindow : Window, new()
{
    private AppBuilder? _builder = null;
    private string[]? _runArgs;
    private ClassicDesktopStyleApplicationLifetime? _lifetime;
    
    private IEnumerable<ModuleBase> _modules;
    
    protected TWindow? MainWindow { get; set; }

    private string[] RunArgs
    {
        get => _runArgs ?? [];
        set => _runArgs = value;
    }

    public virtual void Initialize()
    {
        Common.Logger.InitializeConsoleLogger(LogEventLevel.Verbose);
        Log.Information($"Initializing Application: {MainWindow?.Title ?? "A6ToolKits Application"}");
        
        Log.Information("Finish Initializing Application.");
    }

    public virtual void Configure()
    {
        Log.Information("Configuring Application: Config Avalonia builder & lifetime");
        
        // 如果是 Debug 模式，则启用Avalonia的日志记录
        if (System.Diagnostics.Debugger.IsAttached)
        {
            _builder = AppBuilder
                .Configure<TApplication>()
                .UsePlatformDetect()
                .LogToTrace();
        }
        else
        {
            _builder = AppBuilder
                .Configure<TApplication>()
                .UsePlatformDetect();
        }

        _lifetime = LifetimeExtensions.PrepareLifetime(_builder, RunArgs, null);
        _builder.SetupWithLifetime(_lifetime);
        
        // 加载模块
        _modules = ModuleLoader.LoadModules();
    }

    public virtual void OnCompleted()
    {
        if (_lifetime == null) return;
        MainWindow ??= new TWindow();
        _lifetime.MainWindow = MainWindow;
        
        Log.Information("Finish Configuring Application.");
        
        Log.Information("Starting Application...");
        _lifetime.Start(RunArgs);
    }


    public virtual void Run(string[] args)
    {
        RunArgs = args;
        Initialize();
        Configure();
        OnCompleted();
        Log.Information("Application Stopped.");
    }
}