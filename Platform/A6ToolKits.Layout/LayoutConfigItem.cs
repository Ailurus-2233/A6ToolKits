using A6ToolKits.Helper.Config;
using A6ToolKits.Layout.ControlGenerator;
using A6ToolKits.Layout.Generator;
using A6ToolKits.Layout.Generator.Enums;
using Avalonia.Media;
using Avalonia.Styling;

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
        var config = WindowConfig.Instance;
        config.Title = !string.IsNullOrEmpty(Title) ? Title : string.Empty;
        config.Width = !string.IsNullOrEmpty(Width) ? double.Parse(Width) : 0;
        config.Height = !string.IsNullOrEmpty(Height) ? double.Parse(Height) : 0;
        config.PrimaryColor =
            Color.TryParse(PrimaryColor, out var primaryColor)
                ? Color.FromRgb(primaryColor.R, primaryColor.G, primaryColor.B)
                : Colors.CornflowerBlue;
        config.BackgroundColor =
            Color.TryParse(BackgroundColor, out var backgroundColor)
                ? Color.FromRgb(backgroundColor.R, backgroundColor.G, backgroundColor.B)
                : Colors.Wheat;
        config.BorderStyle = Enum.TryParse<WindowBorderType>(BorderStyle, out var borderStyle)
            ? borderStyle
            : WindowBorderType.Default;

        var brightness = 0.299 * backgroundColor.R + 0.587 * backgroundColor.G + 0.114 * backgroundColor.B;
        config.Theme = brightness > 128 ? ThemeVariant.Light : ThemeVariant.Dark;
    }
}