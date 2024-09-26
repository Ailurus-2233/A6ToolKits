using A6ToolKits.Common.Attributes.MVVM;
using A6ToolKits.Helper.Instance;
using A6ToolKits.Module;
using Serilog;

namespace A6ToolKits.Layout;

/// <summary>
///     布局模块，如果加载该模块将会基于配置文件自动加载窗口
/// </summary>
[AutoRegister(typeof(LayoutModule), RegisterType.Singleton)]
public class LayoutModule : ModuleBase
{
    /// <summary>
    ///     实例创建器，用于模块内部创建实例
    /// </summary>
    public override IInstanceHelper? Creator { get; set; } = new BaseInstanceHelper();

    /// <summary>
    ///     模块名称
    /// </summary>
    public override string ModuleName { get; set; } = "A6ToolKits.Layout_bk";

    /// <summary>
    ///     模块版本
    /// </summary>
    public override string ModuleVersion { get; set; } = "1.0.0";

    /// <summary>
    ///     模块描述
    /// </summary>
    public override string ModuleDescription { get; set; } =
        "Add Layout capabilities to A6ToolKits to automatically load layout for main window";
    
    
    /// <summary>
    ///     初始化布局模块，加载布局配置文件
    /// </summary>
    /// <exception cref="Exception">
    ///     布局加载失败
    /// </exception>
    protected override void Initialize()
    {
        Log.Information("Load layout from configuration file");
        Log.Information("Load layout success");
    }
}