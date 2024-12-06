using A6ToolKits.Common.ResourceLoader;
using A6ToolKits.Layout.Generator;
using A6ToolKits.Layout.Generator.Enums;
using A6ToolKits.ResourceLoader;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace A6ToolKits.Layout.Controls.LayoutWindow;

/// <summary>
///     窗口基类
/// </summary>
public abstract class WindowBase : Window
{
    /// <summary>
    ///     主区域
    /// </summary>
    public abstract UserControl? MainRegion { get; set; }
    
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
        if (config.Icon == null) return;
        try
        {
            var image = AssetHelper.LoadImage(config.Icon);
            if (image is Bitmap bitmap)
                Icon = new WindowIcon(bitmap);
        }
        catch (Exception e)
        {
            // ToDo Log Filed Load Icon
        }

    }
}