using System;
using System.Collections.Generic;
using A6ToolKits.Bootstrapper;
using A6ToolKits.Bootstrapper.Interfaces;
using A6ToolKits.Instance;

namespace A6ToolKits;

/// <summary>
///     核心服务管理，提供全局的服务管理和应用控制
/// </summary>
public class CoreService
{
    private static readonly Lazy<CoreService> _instance = new(() => new CoreService());

    private CoreService()
    { }

    /// <summary>
    ///     启动服务实例
    /// </summary>
    public static CoreService Instance => _instance.Value;
    
    /// <summary>
    ///     应用控制器
    /// </summary>
    public IBootstrapperController? Controller { get; set; }
    
    /// <summary>
    ///     服务字典，通过服务类获取对应的实例
    /// </summary>
    public Dictionary<Type, object>? Services { get; set; }

    /// <summary>
    ///     实例创建工具
    /// </summary>
    public IInstanceCreator? Creator { get; set; }

    /// <summary>
    ///     初始化CoreService
    /// </summary>
    /// <param name="controller">
    ///     Bootstrapper 控制器
    /// </param>
    public void Initialize(IBootstrapperController controller)
    {
        Services = new Dictionary<Type, object>();
        Creator = new SimpleInstanceCreator();
        Controller = controller;
    }
}