using A6ToolKits.Common.Attributes.MVVM;
using A6ToolKits.Helper.Instance;
using A6ToolKits.Module;
using A6ToolKits.MVVM.Helper;

namespace A6ToolKits.MVVM;

/// <summary>
///     MVVM模块， 用于为A6ToolKits添加MVVM、IOC的能力，自动加载一些服务和View、Viewmodel
/// </summary>
[AutoRegister(typeof(MVVMModule), RegisterType.Singleton)]
public class MVVMModule : ModuleBase
{
    /// <summary>
    ///     实例创建器，用于模块内部创建实例
    /// </summary>
    public override IInstanceHelper? Creator { get; set; } = IoC.Instance;
    
    /// <summary>
    ///     初始化
    /// </summary>
    protected override void Initialize()
    {
        IoCHelper.AutoRegisterAll();
    }
}