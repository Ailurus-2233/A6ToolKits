using System;
using A6ToolKits.Bootstrapper.Interfaces;
using A6ToolKits.Bootstrapper.Utils;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace A6ToolKits.Bootstrapper;

public class BaseBootstrapper<TApplication, TWindow> : IBootstrapper
    where TApplication : Application, new()
    where TWindow : Window, new()
{
    private AppBuilder? _builder = null;
    private string[]? _runArgs;
    private ClassicDesktopStyleApplicationLifetime? _lifetime;

    protected TWindow? MainWindow { get; set; }
    
    private string[] RunArgs
    {
        get => _runArgs ?? [];
        set => _runArgs = value;
    }

    public virtual void Initialize()
    {
    }

    public virtual void Configure()
    {
        _builder = AppBuilder.Configure<TApplication>().UsePlatformDetect().WithInterFont().LogToTrace();
        _lifetime = LifetimeExtensions.PrepareLifetime(_builder, RunArgs, null);
        _builder.SetupWithLifetime(_lifetime);
    }

    public virtual void OnCompleted()
    {
        if (_lifetime == null) return;
        MainWindow ??= new TWindow();
        _lifetime.MainWindow = MainWindow;
        _lifetime.Start(RunArgs);
    }


    public virtual void Run(string[] args)
    {
        RunArgs = args;
        Initialize();
        Configure();
        OnCompleted();
    }
}