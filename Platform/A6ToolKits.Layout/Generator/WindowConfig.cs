using A6ToolKits.Layout.ControlGenerator.Enums;
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
    public static WindowBorderType BorderStyle { get; set; } = WindowBorderType.Default;

    /// <summary>
    ///     菜单控件类型
    /// </summary>
    public static MenuType MenuType
    {
        get
        {
            return BorderStyle switch
            {
                WindowBorderType.A6ToolKits => MenuType.Icon,
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
    
    public static double MenuHeight { get; set; } = 30;
    
    public static double StatusBarHeight { get; set; } = 30;
    
    public static double TopBarHeight { get; set; } = 30;
}