using A6ToolKits.Layout.Container;
using A6ToolKits.Layout.Controls;
using A6ToolKits.Layout.Enums;
using Avalonia.Media;
using Avalonia.Styling;
using DefaultWindow = A6ToolKits.Layout.Controls.DefaultWindow;
using MainPanel = A6ToolKits.Layout.Controls.MainPanel;

namespace A6ToolKits.Layout;

/// <summary>
///     窗口布局，用于管理窗口的布局，包括菜单、左侧、右侧、顶部、底部、状态栏、页面
/// </summary>
public class WindowLayout
{
    /// <summary>
    ///     窗口容器，用于实现具体的布局设置
    /// </summary>
    public DefaultWindow WindowContainer { get; } = new();

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
    public MainPanel MainPanel => WindowContainer.MainPanel;

    /// <summary>
    ///     页面容器，用于展示页面，位置在窗口中间
    /// </summary>
    public PageContainer Container => MainPanel.PageContainer;

    /// <summary>
    ///     窗口类型，默认为 Window
    /// </summary>
    public WindowType Type { get; set; } = WindowType.Window;

    /// <summary>
    ///     窗口宽度，默认为 800
    /// </summary>
    public double Width
    {
        get => WindowContainer.Width;
        set => WindowContainer.Width = value;
    }

    /// <summary>
    ///     窗口高度，默认为 600
    /// </summary>
    public double Height
    {
        get => WindowContainer.Height;
        set => WindowContainer.Height = value;
    }

    /// <summary>
    ///     背景色，会修改部分生成控件的颜色，默认为 CornflowerBlue
    /// </summary>
    public Color BackgroundColor { get; set; } = Color.Parse("#181818");

    /// <summary>
    ///     主题色，会修改部分生成控件的颜色，默认为 CornflowerBlue
    /// </summary>
    public Color PrimaryColor { get; set; } = Color.Parse("#3377D2");

    /// <summary>
    ///     全局主题，根据主题色生成，如果主题色为深色则生成深色主题，否则生成浅色主题
    /// </summary>
    public ThemeVariant Theme =>
        0.299 * BackgroundColor.R + 0.587 * BackgroundColor.G + 0.114 * BackgroundColor.B > 128
            ? ThemeVariant.Light
            : ThemeVariant.Dark;
}