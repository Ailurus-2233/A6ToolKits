using System;
using System.Diagnostics;
using A6ToolKits.Bootstrapper.Extensions;
using A6ToolKits.Bootstrapper.Interfaces;
using A6ToolKits.Helper;
using A6ToolKits.Helper.Assembly;
using A6ToolKits.Helper.Logger;
using A6ToolKits.Module;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Styling;
using Serilog;
using LogEventLevel = Serilog.Events.LogEventLevel;

namespace A6ToolKits.Bootstrapper;

/// <summary>
///     应用程序启动类，用于初始化Avalonia应用程序，并加载配置文件中指定的模块
/// </summary>
/// <typeparam name="TApplication">
///     应用程序类型，指定一个 Application 类型作为应用启动类
/// </typeparam>
/// <typeparam name="TWindow">
///     主窗体类型，指定一个 Window 类型作为主窗体
/// </typeparam>
public class BaseBootstrapper<TApplication, TWindow> : IBootstrapper, IApplicationController
    where TApplication : Application, new()
    where TWindow : Window, new()
{
    private ThemeVariant _theme = ThemeVariant.Default;

    /// <summary>
    ///    主题设置，默认为 Dark
    /// </summary>
    public ThemeVariant Theme
    {
        get => _theme;
        set
        {
            if (value == _theme) return;
            _theme = value;
            if (MainWindow != null)
                MainWindow.RequestedThemeVariant = _theme;

        }
    }

    /// <summary>
    ///     主窗体对象，通过修改此属性可以设置主窗体，但已经显示的窗体无法修改
    /// </summary>
    public TWindow? MainWindow { get; set; }

    /// <summary>
    ///     通用窗口接口
    /// </summary>
    public Window? Window
    {
        get => MainWindow;
        set => MainWindow = (TWindow?)value;
    }
    
    /// <summary>
    ///     构造器，用于子类进行扩展修改
    /// </summary>
    public AppBuilder? AppBuilder { get; set; }
    
    /// <summary>
    ///     执行 Avalonia 应用的参数
    /// </summary>
    public string[]? RunArguments { get; set; }
    
    /// <summary>
    ///     Avalonia 桌面应用的生命周期 
    /// </summary>
    public ClassicDesktopStyleApplicationLifetime? ApplicationLifetime { get; set; }

    /// <summary>
    ///     加载步骤1-初始化：在应用启动前需要进行的一些配置，这里进行了程序集加载和
    ///     日志记录器的初始化，可以在子类中重写此方法以添加更多的初始化操作
    /// </summary>
    public virtual void Initialize()
    {
        BootstrapperService.Instance.ApplicationController = this;
        // 加载程序集
        AssemblyHelper.LoadAssemblyPath();
        AppDomain.CurrentDomain.AssemblyResolve += AssemblyHelper.OnResolveAssembly;

        // 初始化日志记录器
        LoggerHelper.InitializeConsoleLogger(LogEventLevel.Verbose);

        Log.Information($"Initializing Application: {MainWindow?.Title ?? "A6ToolKits Application"}");

        // 自动加载配置文件中Assembly路径
        Log.Information("Loading Assembly Path from configuration file");

        Log.Information("Finish Initializing Application.");
    }

    /// <summary>
    ///     加载步骤2-配置：在初始化完成后，需要进行的一些配置，这里进行了 Avalonia
    ///     构建器的配置，配置完成后加载模块列表，可以在子类中重写此方法以添加更多的
    ///     配置操作
    /// </summary>
    public virtual void Configure()
    {
        Log.Information("Configuring Application: Config Avalonia builder & lifetime");
        AvaloniaConfigure();
        // 加载模块
        ModuleLoader.LoadModules();
    }

    /// <summary>
    ///     Avalonia 框架的相关配置
    /// </summary>
    protected virtual void AvaloniaConfigure()
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
        
        ApplicationLifetime = LifetimeExtensions.PrepareLifetime(AppBuilder, RunArguments ?? [], null);
        AppBuilder.SetupWithLifetime(ApplicationLifetime);
    }

    /// <summary>
    ///     加载步骤3-完成：在配置完成后，需要进行的一些操作，这里设置了主窗体对象，
    ///     最好在子类中重写此方法以设置主窗体对象，后调用基类的此方法以启动应用程序
    /// </summary>
    public virtual void OnCompleted()
    {
        if (ApplicationLifetime == null) return;
        MainWindow ??= new TWindow();
        MainWindow.RequestedThemeVariant = Theme;
        ApplicationLifetime.MainWindow = MainWindow;
        Log.Information("Finish Configuring Application.");

        if (Debugger.IsAttached)
        {
            MainWindow.AttachDevTools();
        }
        
        Log.Information("Starting Application...");
        ApplicationLifetime.Start(RunArguments ?? []);
    }

    /// <summary>
    ///     结束步骤：在程序退出时，需要执行的一些操作，这里输出了程序结束的日志
    /// </summary>
    public virtual void OnFinished()
    {
        Log.Information("Application Stopped.");
    }

    /// <summary>
    ///     停止应用程序
    /// </summary>
    public virtual void StopApplication()
    {
        ApplicationLifetime?.Shutdown();
    }
    
    /// <summary>
    ///     应用的启动方法，会依次调用 Initialize、Configure 和 OnCompleted 方法
    /// </summary>
    /// <param name="args">
    ///     命令行参数
    /// </param>
    public void Run(string[] args)
    {
        RunArguments = args;
        Initialize();
        Configure();
        OnCompleted();
        OnFinished();
    }
}