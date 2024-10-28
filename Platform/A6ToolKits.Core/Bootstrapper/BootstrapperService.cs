using System;
using A6ToolKits.Bootstrapper.Interfaces;
using Avalonia.Controls;

namespace A6ToolKits.Bootstrapper;

/// <summary>
/// 
/// </summary>
public class BootstrapperService
{
    private static readonly Lazy<BootstrapperService> _instance = new(() => new BootstrapperService());

    private BootstrapperService()
    { }

    /// <summary>
    ///     启动服务实例
    /// </summary>
    public static BootstrapperService Instance => _instance.Value;
    
    /// <summary>
    ///     应用控制器
    /// </summary>
    public IApplicationController? ApplicationController { get; set; }
}