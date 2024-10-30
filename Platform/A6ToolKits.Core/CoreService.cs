using System;
using System.Collections.Generic;
using A6ToolKits.Bootstrapper;
using A6ToolKits.Bootstrapper.Interfaces;
using A6ToolKits.Event;
using A6ToolKits.Instance;

namespace A6ToolKits;

/// <summary>
///     核心服务管理，提供全局的服务管理和应用控制
/// </summary>
public class CoreService
{
    private static readonly Lazy<CoreService> instance = new(() => new CoreService());

    private CoreService()
    {
    }

    /// <summary>
    ///     启动服务实例
    /// </summary>
    public static CoreService Instance => instance.Value;

    /// <summary>
    ///     应用控制器
    /// </summary>
    public IBootstrapperController? Controller { get; set; }

    /// <summary>
    ///     实例创建器
    /// </summary>
    public IInstanceCreator? Creator { get; set; }

    /// <summary>
    ///     核心模块
    /// </summary>
    public CoreModule? CoreModule { get; set; }

    /// <summary>
    ///     事件聚合器
    /// </summary>
    public IEventAggregator? EventAggregator { get; set; }

    /// <summary>
    ///     初始化CoreService
    /// </summary>
    /// <param name="controller">
    ///     Bootstrapper 控制器
    /// </param>
    public void Initialize(IBootstrapperController controller)
    {
        Controller = controller;
        EventAggregator = new EventAggregator();
        Creator = new SimpleInstanceCreator();
        CoreModule = new CoreModule();
    }
}