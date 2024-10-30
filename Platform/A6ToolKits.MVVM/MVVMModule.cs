using A6ToolKits.Common.Attributes.MVVM;
using A6ToolKits.Event;
using A6ToolKits.Instance;
using A6ToolKits.Module;
using A6ToolKits.MVVM.Helper;

namespace A6ToolKits.MVVM;

/// <summary>
///     MVVM模块， 用于为A6ToolKits添加MVVM、IOC的能力，自动加载一些服务和View、Viewmodel
/// </summary>
public sealed class MVVMModule : ModuleBase
{
    /// <summary>
    /// 
    /// </summary>
    public override string Name => "A6ToolKits.MVVM";

    /// <summary>
    ///     初始化
    /// </summary>
    public override void Initialize()
    {
        CoreService.Instance.Creator = MVVMCreator.Instance;
        
        if (CoreService.Instance.EventAggregator != null)
            IoC.AddSingleton(CoreService.Instance.EventAggregator);
        if (CoreService.Instance.CoreModule != null)
            IoC.AddSingleton(CoreService.Instance.CoreModule);
        if (CoreService.Instance.Controller != null)
            IoC.AddSingleton(CoreService.Instance.Controller);
        
        IoCHelper.AutoRegisterAll();
    }
}