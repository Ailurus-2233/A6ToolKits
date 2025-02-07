using System.Diagnostics;
using A6ToolKits.ApplicationController;
using A6ToolKits.Container;
using A6ToolKits.EventAggregator;
using A6ToolKits.Events;
using A6ToolKits.Starter;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;
namespace A6ToolKits;

/// <summary>
///     应用程序启动类，用于初始化 Avalonia 应用程序，并加载配置文件中指定的模块
/// </summary>
/// <typeparam name="TApplication">
///     应用程序类型，指定一个 Application 类型作为应用启动类
/// </typeparam>
/// <typeparam name="TWindow">
///     主窗体类型，指定一个 Window 类型作为主窗体
/// </typeparam>
public abstract class BaseBootstrapper<TApplication, TWindow> : ApplicationControllerBase<TApplication, TWindow>,
    IBootstrapper
    where TApplication : Application, new()
    where TWindow : Window, new()
{
    /// <summary>
    ///     加载步骤1-初始化：在应用启动前需要进行的一些配置，这里进行了程序集加载和
    ///     日志记录器的初始化，可以在子类中重写此方法以添加更多的初始化操作
    /// </summary>
    public virtual void Initialize()
    {
    }

    /// <summary>
    ///     加载步骤2-配置：在初始化完成后，需要进行的一些配置，这里进行了 Avalonia
    ///     构建器的配置，配置完成后加载模块列表，可以在子类中重写此方法以添加更多的
    ///     配置操作
    /// </summary>
    public virtual void Configure()
    {
        AutomaticRegister.AutoRegister();
        ViewModelAutomaticRegister.AutoRegister();
        IoC.RegisterSingleton<IApplicationController>(this);
        IoC.RegisterSingleton<IBootstrapper>(this);
        LoadResources();
    }

    /// <summary>
    ///     加载步骤3-完成：在配置完成后，需要进行的一些操作，这里设置了主窗体对象，
    ///     最好在子类中重写此方法以设置主窗体对象，后调用基类的此方法以启动应用程序
    /// </summary>
    public virtual void OnCompleted()
    {
        if (Lifetime == null) return;
        MainWindow ??= new TWindow();
        Lifetime.MainWindow = MainWindow;
        if (Debugger.IsAttached) MainWindow.AttachDevTools();
        IoC.GetInstance<IEventAggregator>()?.Publish(new BootFinishedEvent());
        Lifetime.Start(RunArguments ?? []);
    }

    /// <summary>
    ///     加载资源文件：加载应用程序的资源文件，这里加载了 A6ToolKits 库的资源文件
    /// </summary>
    private static void LoadResources()
    {
        var resUri = new Uri("avares://A6ToolKits.Resources/Resources.axaml");
        var resource = new ResourceInclude(resUri) { Source = resUri };
        Application.Current?.Resources.MergedDictionaries.Add(resource);
    }

    /// <summary>
    ///     结束步骤：在程序退出时，需要执行的一些操作
    /// </summary>
    public virtual void OnFinished()
    {
        IoC.GetInstance<IEventAggregator>()?.Publish(new ApplicationExitEvent());
    }

    /// <summary>
    ///     应用的启动方法，会依次调用 Initialize、Configure 和 OnCompleted 方法
    /// </summary>
    /// <param name="args">
    ///     命令行参数
    /// </param>
    public override void Run(string[] args)
    {
        base.Run(args);
        Initialize();
        Configure();
        OnCompleted();
        OnFinished();
    }
}