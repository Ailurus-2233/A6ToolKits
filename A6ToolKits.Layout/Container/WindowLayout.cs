using A6ToolKits.Layout.Container.Controls;
using Avalonia.Controls;
using Avalonia.Media;

namespace A6ToolKits.Layout.Container;

/// <summary>
///     窗口布局，用于管理窗口的布局，包括菜单、左侧、右侧、顶部、底部、状态栏、页面
/// </summary>
public class WindowLayout
{
    private PageContainer? _container;

    /// <summary>
    ///     窗口容器，用于实现具体的布局设置
    /// </summary>
    public WindowContainer WindowContainer { get; } = new();

    /// <summary>
    ///     顶部面板，是菜单、工具栏、大图标工具栏的容器
    /// </summary>
    public DockPanel TopPanel => WindowContainer.TopPanel;

    /// <summary>
    ///     菜单，根据菜单项生成，位置在窗口顶部
    /// </summary>
    public Menu Menu => WindowContainer.Menu;

    /// <summary>
    ///     工具栏，用于展示一些工具按钮，位置在窗口顶部
    /// </summary>
    public StackPanel ToolBar => WindowContainer.ToolBar;


    /// <summary>
    ///     大图标工具栏，用于展示一些工具按钮，位置在窗口顶部右侧
    /// </summary>
    public StackPanel RightToolBar => WindowContainer.RightToolBar;

    /// <summary>
    ///     状态栏，用于展示一些状态信息，位置在窗口底部，包括左侧、中间、右侧
    /// </summary>
    public DockPanel StatusBar => WindowContainer.StatusBar;

    /// <summary>
    ///     状态栏，用于展示一些状态信息，位置在窗口底部左侧
    /// </summary>
    public StackPanel LeftStatusBar => WindowContainer.LeftStatus;

    /// <summary>
    ///     状态栏，用于展示一些状态信息，位置在窗口底部中间
    /// </summary>
    public StackPanel CenterStatusBar => WindowContainer.CenterStatus;

    /// <summary>
    ///     状态栏，用于展示一些状态信息，位置在窗口底部右侧
    /// </summary>
    public StackPanel RightStatusBar => WindowContainer.RightStatus;


    /// <summary>
    ///     主面板，用于展示页面，位置在窗口中间
    /// </summary>
    public DockPanel MainPanel => WindowContainer.MainPanel;

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
    public PageContainer Container
    {
        get
        {
            if (_container != null) return _container;
            _container = new PageContainer();
            MainPanel.Children.Add(_container);
            return _container;
        }
    }
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