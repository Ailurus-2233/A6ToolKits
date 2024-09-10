using A6ToolKits.Layout.Container.Controls;
using Avalonia.Controls;
using Avalonia.Media;

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
    ///     菜单，根据菜单项生成，位置在窗口顶部
    /// </summary>
    public Menu Menu => WindowContainer.Menu;

    /// <summary>
    ///     左侧面板，用于放置一些控件，位置在窗口左侧
    /// </summary>
    public StackPanel LeftPanel => WindowContainer.LeftDock;

    /// <summary>
    ///     右侧面板，用于放置一些控件，位置在窗口右侧
    /// </summary>
    public StackPanel RightPanel => WindowContainer.RightDock;

    /// <summary>
    ///     顶部面板，用于放置一些控件，位置在窗口顶部，菜单下方
    /// </summary>
    public StackPanel TopPanel => WindowContainer.TopDock;

    /// <summary>
    ///     底部面板，用于放置一些控件，位置在窗口底部，状态栏上方
    /// </summary>
    public StackPanel BottomPanel => WindowContainer.BottomDock;

    /// <summary>
    ///     状态栏，用于展示一些状态信息，位置在窗口底部
    /// </summary>
    public DockPanel StatusBar => WindowContainer.StatusBar;

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
    ///     页面容器，用于展示页面，位置在窗口中间
    /// </summary>
    public PageContainer Container => WindowContainer.Page;
}

/// <summary>
///     窗口类型
/// </summary>
public enum WindowType
{
    /// <summary>
    ///     窗口
    /// </summary>
    Window,

    /// <summary>
    ///     全屏
    /// </summary>
    FullScreen
}