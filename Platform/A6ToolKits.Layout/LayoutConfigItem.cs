using A6ToolKits.Helper.Config;
using A6ToolKits.Layout.ControlGenerator;
using A6ToolKits.Layout.Generator;
using A6ToolKits.Layout.Generator.Enums;
using Avalonia.Media;

namespace A6ToolKits.Layout;

/// <summary>
///     布局配置项
/// </summary>
public class LayoutConfigItem : ConfigItemBase
{
    /// <summary>
    ///     窗口标题
    /// </summary>
    public string Title { get; set; } = "";

    /// <summary>
    ///     窗口边框样式
    /// </summary>
    public string BorderStyle { get; set; } = "";

    /// <summary>
    ///     窗口宽度
    /// </summary>
    public string Width { get; set; } = "";

    /// <summary>
    ///     窗口高度
    /// </summary>
    public string Height { get; set; } = "";

    /// <summary>
    ///     窗口重点色
    /// </summary>
    public string PrimaryColor { get; set; } = "";

    /// <summary>
    ///     窗口背景色
    /// </summary>
    public string BackgroundColor { get; set; } = "";

    internal void SetToResources()
    {
        WindowConfig.Title = !string.IsNullOrEmpty(Title) ? Title : string.Empty;
        WindowConfig.Width = !string.IsNullOrEmpty(Width) ? double.Parse(Width) : 0;
        WindowConfig.Height = !string.IsNullOrEmpty(Height) ? double.Parse(Height) : 0;
        WindowConfig.PrimaryColor =
            Color.TryParse(PrimaryColor, out var primaryColor) ? primaryColor : Colors.CornflowerBlue;
        WindowConfig.BackgroundColor =
            Color.TryParse(BackgroundColor, out var backgroundColor) ? backgroundColor : Colors.Wheat;
        WindowConfig.BorderStyle = Enum.TryParse<WindowBorderType>(BorderStyle, out var borderStyle)
            ? borderStyle
            : WindowBorderType.Default;
    }
}