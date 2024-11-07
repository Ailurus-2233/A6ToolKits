using System;
using System.Collections.Generic;
using System.Diagnostics;
using A6ToolKits.Common.Events;
using A6ToolKits.Container;
using A6ToolKits.Helper.AssemblyManager;
using A6ToolKits.LifetimeExtension;
using A6ToolKits.Modules;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Styling;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable VirtualMemberNeverOverridden.Global

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
public abstract class BaseBootstrapper<TApplication, TWindow> : IBootstrapper, IWindowController, IModuleManager, IAssemblyManager
    where TApplication : Application, new()
    where TWindow : Window, new()
{
    /// <summary>
    ///     构造器，用于子类进行扩展修改
    /// </summary>
    private AppBuilder? _appBuilder;

    /// <summary>
    ///     Avalonia 桌面应用的生命周期
    /// </summary>
    private ClassicDesktopStyleApplicationLifetime? _lifetime;

    /// <summary>
    ///     主窗体对象，通过修改此属性可以设置主窗体，但已经显示的窗体无法修改
    /// </summary>
    private TWindow? _mainWindow;

    /// <summary>
    ///     执行 Avalonia 应用的参数
    /// </summary>
    private string[]? _runArguments;

    /// <summary>
    ///     停止应用程序
    /// </summary>
    public virtual void StopApplication()
    {
        _lifetime?.Shutdown();
    }

    /// <summary>
    ///     获取主要窗口
    /// </summary>
    /// <returns>
    ///     如果内部窗口字段不为空，则返回内部窗口，否则返回一个新的窗口
    /// </returns>
    public Window GetMainWindow()
    {
        return _mainWindow ??= new TWindow();
    }

    /// <summary>
    ///     设置主要窗口，仅在初始化之前可以设置
    /// </summary>
    /// <param name="mainWindow">
    ///     输入的主要窗口
    /// </param>
    public void SetupMainWindow(Window mainWindow)
    {
        if (_mainWindow != null)
            throw new InvalidOperationException("The main window already set.");
        if (mainWindow is TWindow window)
            _mainWindow = window;
        else
            throw new InvalidOperationException("The main window is not of type TWindow.");
    }

    /// <summary>
    ///     设置应用的主题
    /// </summary>
    /// <param name="theme">
    ///     输入的主题：Light,Dark,Default
    /// </param>
    public void SetupTheme(ThemeVariant theme)
    {
        if (_mainWindow != null)
            _mainWindow.RequestedThemeVariant = theme;
        if (Application.Current != null)
            Application.Current.RequestedThemeVariant = theme;
    }

    /// <summary>
    ///     需要加载的模块
    /// </summary>
    protected abstract List<Type> ToLoadModules { get; }

    /// <summary>
    ///     Assembly 加载的路径
    /// </summary>
    protected List<string> ToLoadAssemblyPaths { get; } = ["./"];

    /// <summary>
    ///     应用的启动方法，会依次调用 Initialize、Configure 和 OnCompleted 方法
    /// </summary>
    /// <param name="args">
    ///     命令行参数
    /// </param>
    public void Run(string[] args)
    {
        _runArguments = args;
        Initialize();
        Configure();
        OnCompleted();
        OnFinished();
    }

    /// <summary>
    ///     加载步骤1-初始化：在应用启动前需要进行的一些配置，这里进行了程序集加载和
    ///     日志记录器的初始化，可以在子类中重写此方法以添加更多的初始化操作
    /// </summary>
    public virtual void Initialize()
    {
        LoadHelper.AssemblyPaths = ToLoadAssemblyPaths;
        LoadHelper.ResolveAssembly();
        AvaloniaConfigure();
    }

    /// <summary>
    ///     加载步骤2-配置：在初始化完成后，需要进行的一些配置，这里进行了 Avalonia
    ///     构建器的配置，配置完成后加载模块列表，可以在子类中重写此方法以添加更多的
    ///     配置操作
    /// </summary>
    public virtual void Configure()
    {
        AutomaticRegister.AutoRegisterAll();
        IoC.RegisterSingleton<IWindowController>(this);
        IoC.RegisterSingleton<IModuleManager>(this);
        IoC.RegisterSingleton<IAssemblyManager>(this);
        IoC.GetInstance<CoreModule>()?.LoadModule();
    }

    /// <summary>
    ///     加载步骤3-完成：在配置完成后，需要进行的一些操作，这里设置了主窗体对象，
    ///     最好在子类中重写此方法以设置主窗体对象，后调用基类的此方法以启动应用程序
    /// </summary>
    public virtual void OnCompleted()
    {
        if (_lifetime == null) return;
        _mainWindow ??= new TWindow();
        _lifetime.MainWindow = _mainWindow;
        if (Debugger.IsAttached) _mainWindow.AttachDevTools();
        IoC.GetInstance<IEventAggregator>()?.Publish(new BootFinishedEvent());
        _lifetime.Start(_runArguments ?? []);
    }

    /// <summary>
    ///     结束步骤：在程序退出时，需要执行的一些操作，这里输出了程序结束的日志
    /// </summary>
    public virtual void OnFinished()
    {
    }

    /// <summary>
    ///     Avalonia 框架的相关配置
    /// </summary>
    protected virtual void AvaloniaConfigure()
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

    /// <summary>
    ///     获取模块列表
    /// </summary>
    /// <returns>
    ///     模块的类型列表
    /// </returns>
    public List<Type> GetModulesType()
    {
        return ToLoadModules;
    }
    
    /// <summary>
    ///     获取模块列表
    /// </summary>
    /// <returns>
    ///     模块的类型列表
    /// </returns>
    public List<ModuleBase> GetLoadedModules()
    {
        var result = new List<ModuleBase>();
        ToLoadModules.ForEach(type =>
        {
            if (IoC.GetInstance(type) is ModuleBase module)
                result.Add(module);
        });
        return result;
    }

    /// <summary>
    ///     获取程序集路径
    /// </summary>
    /// <returns>
    ///     程序集路径列表
    /// </returns>
    public List<string> GetAssemblyPaths()
    {
        return ToLoadAssemblyPaths;
    }
}