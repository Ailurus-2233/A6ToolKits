using A6ToolKits.Layout.Generator.Enums;
using Avalonia.Media;
namespace A6ToolKits.Layout.Generator;

/// <summary>
///     生成器的资源
/// </summary>
public static class WindowConfig
{
    /// <summary>
    ///     窗口标题
    /// </summary>
    public static string Title { get; set; } = "A6ToolKits";

    /// <summary>
    ///     窗口边框类型
    /// </summary>
    public static WindowBorderType BorderStyle { get; set; } = WindowBorderType.Origin;

    /// <summary>
    ///     窗口类型
    /// </summary>
    public static WindowType WindowType { get; set; } = WindowType.Default;

    /// <summary>
    ///     菜单控件类型
    /// </summary>
    public static MenuType MenuType
    {
        get
        {
            return BorderStyle switch
            {
                WindowBorderType.Default => MenuType.Icon,
                _ => MenuType.Row
            };
        }
    }

    /// <summary>
    ///     窗口宽度
    /// </summary>
    public static double Width { get; set; } = 800;

    /// <summary>
    ///     窗口高度
    /// </summary>
    public static double Height { get; set; } = 600;

    /// <summary>
    ///     窗口重点色
    /// </summary>
    public static Color PrimaryColor { get; set; } = Colors.CornflowerBlue;
    
    /// <summary>
    ///     窗口背景色
    /// </summary>
    public static Color BackgroundColor { get; set; } = Colors.Wheat;

    /// <summary>
    ///     菜单栏高度
    /// </summary>
    public static double MenuHeight { get; set; } = 30;

    /// <summary>
    ///     状态栏高度
    /// </summary>
    public static double StatusBarHeight { get; set; } = 30;

    /// <summary>
    ///     工具栏高度
    /// </summary>
    public static double ToolBarHeight { get; set; } = 30;


}