using A6ToolKits.Layout.Container.Controls;
using A6ToolKits.Layout.Container.Controls.Enums;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;

namespace A6ToolKits.Layout.Container;

/// <summary>
///     窗口布局，用于管理窗口的布局，包括菜单、左侧、右侧、顶部、底部、状态栏、页面
/// </summary>
public class WindowLayout
{
    /// <summary>
    ///     窗口容器，用于实现具体的布局设置
    /// </summary>
    public WindowContainer WindowContainer { get; } = new();

    /// <summary>
    ///     顶部面板，是菜单、工具栏、大图标工具栏的容器
    /// </summary>
    public HeaderBar HeaderBar => WindowContainer.HeaderBar;

    /// <summary>
    ///     状态栏，用于展示一些状态信息，位置在窗口底部，包括左侧、中间、右侧
    /// </summary>
    public StatusBar StatusBar => WindowContainer.StatusBar;

    /// <summary>
    ///     主面板，用于展示页面，位置在窗口中间
    /// </summary>
    public MainPanel? MainPanel { get; set; }

    /// <summary>
    ///     页面容器，用于展示页面，位置在窗口中间
    /// </summary>
    public PageContainer? Container => MainPanel?.PageContainer;

    /// <summary>
    ///     窗口类型，默认为 Window
    /// </summary>
    public WindowType Type { get; set; } = WindowType.Window;

    /// <summary>
    ///     窗口宽度，默认为 800
    /// </summary>
    public double Width { get; set; } = 800;

    /// <summary>
    ///     窗口高度，默认为 600
    /// </summary>
    public double Height { get; set; } = 600;

    /// <summary>
    ///     主题色，会修改部分生成控件的颜色，默认为 CornflowerBlue
    /// </summary>
    public Color MainColor { get; set; } = Colors.CornflowerBlue;

    /// <summary>
    ///     全局主题，根据主题色生成，如果主题色为深色则生成深色主题，否则生成浅色主题
    /// </summary>
    public ThemeVariant Theme =>
        0.299 * MainColor.R + 0.587 * MainColor.G + 0.114 * MainColor.B > 128 ? ThemeVariant.Light : ThemeVariant.Dark;
}