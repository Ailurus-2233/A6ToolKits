using A6ToolKits.Layout.Generator;
using A6ToolKits.Layout.Generator.Enums;
using Avalonia.Controls;
using Avalonia.Media;

namespace A6ToolKits.Layout.Controls.LayoutWindow;

/// <summary>
///     窗口基类
/// </summary>
public abstract class WindowBase : Window
{
    /// <summary>
    ///     窗口基类构造函数
    /// </summary>
    protected WindowBase()
    {
        var config = WindowConfig.Instance;
        Title = config.Title;
        Height = config.Height;
        Width = config.Width;
        Background = Brushes.Transparent;
        WindowState = config.WindowType switch
        {
            WindowType.Default => WindowState.Normal,
            WindowType.Maximized => WindowState.Maximized,
            WindowType.FullScreen => WindowState.FullScreen,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}