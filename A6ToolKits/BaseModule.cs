using A6ToolKits.Common.Attributes.MVVM;
using A6ToolKits.Helper.Instance;
using A6ToolKits.Module;

namespace A6ToolKits;

/// <summary>
///     核心模块
/// </summary>
[AutoRegister(typeof(BaseModule), RegisterType.Singleton)]
public class BaseModule : ModuleBase
{
    /// <summary>
    ///     实例创建器，用于模块内部创建实例
    /// </summary>
    public override IInstanceHelper? Creator { get; set; } = new BaseInstanceHelper();

    /// <summary>
    ///     模块名称
    /// </summary>
    public override string ModuleName { get; set; } = "A6ToolKits.Root";

    /// <summary>
    ///     模块版本
    /// </summary>
    public override string ModuleVersion { get; set; } = "1.0.0";

    /// <summary>
    ///     模块描述
    /// </summary>
    public override string ModuleDescription { get; set; } = "Core of A6ToolKits framework";

    /// <summary>
    ///     初始化，加载模块时执行的操作
    /// </summary>
    protected override void Initialize()
    {
        // 自动加载 CoreControls.axaml 到 Avalonia 的资源字典中
        // var styleUri = new Uri("avares://A6ToolKits/CoreControls.axaml");
        // var style = new StyleInclude(styleUri)
        // {
        //     Source = styleUri
        // };
        // Application.Current?.Styles.Add(style);
    }
}