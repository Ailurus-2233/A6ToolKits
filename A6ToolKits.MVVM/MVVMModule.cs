using A6ToolKits.Attributes;
using A6ToolKits.InstanceCreator;
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
    public override IInstanceCreator? Creator { get; set; } = IoC.Instance;

    /// <summary>
    ///     模块名称
    /// </summary>
    public override required string ModuleName { get; set; } = "A6ToolKits.MVVM";

    /// <summary>
    ///     模块版本
    /// </summary>
    public override required string ModuleVersion { get; set; } = "1.0.0";

    /// <summary>
    ///     模块描述
    /// </summary>
    public override required string ModuleDescription { get; set; } =
        "Add MVVM, IOC capabilities to A6ToolKits to automatically load some services and view & Viewmodel";

    /// <summary>
    ///     初始化
    /// </summary>
    protected override void Initialize()
    {
        IoCHelper.AutoRegisterAll();
    }
}