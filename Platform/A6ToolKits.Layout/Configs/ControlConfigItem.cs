using A6ToolKits.Configuration;
using A6ToolKits.Configuration.Attributes;

namespace A6ToolKits.Layout.Configs;

/// <summary>
///     控件配置项
/// </summary>
[ConfigName("Control")]
public class ControlConfigItem : ConfigItemBase
{
    /// <summary>
    ///     窗口重点色
    /// </summary>
    public string PrimaryColor { get; set; } = "#6495ED";

    /// <summary>
    ///     窗口背景色
    /// </summary>
    public string BackgroundColor { get; set; } = "#FFFFFF";

    /// <summary>
    ///     菜单栏高度
    /// </summary>
    public string MenuHeight { get; set; } = "30";

    /// <summary>
    ///     状态栏高度
    /// </summary>
    public string StatusBarHeight { get; set; } = "30";

    /// <summary>
    ///     工具栏高度
    /// </summary>
    public string ToolBarHeight { get; set; } = "30";

    /// <summary>
    ///     标题栏高度，仅在 Default 布局下生效
    /// </summary>
    public string TitleBarHeight { get; set; } = "30";

    /// <inheritdoc />
    public override bool IsNecessary => true;

    /// <inheritdoc />
    public override void SetDefault()
    {
        PrimaryColor = "#6495ED";
        BackgroundColor = "#FFFFFF";
        MenuHeight = "30";
        StatusBarHeight = "30";
        ToolBarHeight = "30";
        TitleBarHeight = "30";
        Children.Clear();
    }
}