using A6ToolKits.Helper.Config;

namespace A6ToolKits.Layout.Helper;

/// <summary>
///     布局项配置项
/// </summary>
public class LayoutItemConfigItem : ConfigItemBase
{
    /// <summary>
    ///     控件加载的程序集
    /// </summary>
    public string Assembly { get; set; } = string.Empty;

    /// <summary>
    ///     控件的具体类
    /// </summary>
    public string Target { get; set; } = string.Empty;

    /// <summary>
    ///     窗口类型
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    ///     窗口宽高
    /// </summary>
    public string Height { get; set; } = string.Empty;

    /// <summary>
    ///     窗口宽高
    /// </summary>
    public string Width { get; set; } = string.Empty;

    /// <summary>
    ///     主题颜色
    /// </summary>
    public string MainColor { get; set; } = string.Empty;

    /// <summary>
    ///     图标路径
    /// </summary>
    public string IconPath { get; set; } = string.Empty;

    /// <summary>
    ///     控件位置，对 Items 有效
    /// </summary>
    public string Position { get; set; } = string.Empty;

    /// <summary>
    ///     页面名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     默认页面，对 Pages 有效
    /// </summary>
    public string Default { get; set; } = string.Empty;
}