using A6ToolKits.Layout.Generator.Enums;
using Avalonia.Media;
using Avalonia.Styling;

namespace A6ToolKits.Layout.Generator;

/// <summary>
///     生成器的资源
/// </summary>
internal class WindowConfig
{
    private static readonly Lazy<WindowConfig> lazy = new(() => new WindowConfig());

    private WindowConfig()
    {
    }

    /// <summary>
    ///     窗口配置实例
    /// </summary>
    public static WindowConfig Instance => lazy.Value;

    /// <summary>
    ///     窗口标题
    /// </summary>
    public string Title { get; set; } = "A6ToolKits";

    /// <summary>
    ///     窗口边框类型
    /// </summary>
    public WindowBorderType BorderStyle { get; set; } = WindowBorderType.Origin;

    /// <summary>
    ///     窗口类型
    /// </summary>
    public WindowType WindowType { get; set; } = WindowType.Default;

    /// <summary>
    ///     菜单控件类型
    /// </summary>
    public MenuType MenuType
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
    public double Width { get; set; } = 800;

    /// <summary>
    ///     窗口高度
    /// </summary>
    public double Height { get; set; } = 600;

    /// <summary>
    ///     窗口重点色
    /// </summary>
    public Color PrimaryColor { get; set; } = Colors.CornflowerBlue;

    public Color SecondaryColor => Color.FromArgb(100, PrimaryColor.R, PrimaryColor.G, PrimaryColor.B);
    public Color TertiaryColor => Color.FromArgb(50, PrimaryColor.R, PrimaryColor.G, PrimaryColor.B);

    /// <summary>
    ///     窗口背景色
    /// </summary>
    public Color BackgroundColor { get; set; } = Colors.Wheat;

    /// <summary>
    ///     菜单栏高度
    /// </summary>
    public double MenuHeight { get; set; } = 30;

    /// <summary>
    ///     状态栏高度
    /// </summary>
    public double StatusBarHeight { get; set; } = 30;

    /// <summary>
    ///     工具栏高度
    /// </summary>
    public double ToolBarHeight { get; set; } = 30;

    /// <summary>
    ///     标题栏高度，仅在 Default 布局下生效
    /// </summary>
    public double TitleBarHeight { get; set; } = 30;

    /// <summary>
    ///     窗口图标
    /// </summary>
    public Uri Icon { get; set; }

    /// <summary>
    ///     应用主题
    /// </summary>
    public ThemeVariant Theme { get; set; } = ThemeVariant.Default;
}