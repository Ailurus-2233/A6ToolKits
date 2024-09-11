using A6ToolKits.Module;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace A6ToolKits.Layout.Container.Controls;

/// <summary>
///     窗口容器，基于该窗口实现具体的布局设置
/// </summary>
public partial class WindowContainer : Window
{
    /// <summary>
    ///     构造函数
    /// </summary>
    public WindowContainer()
    {
        InitializeComponent();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (!ModuleLoader.TryGetModule<LayoutModule>(out var layoutModule)) return;
        var color = layoutModule?.WindowLayout?.MainColor;
        if (color == null) return;
        var brush = new SolidColorBrush(color.Value);
        TopPanel.Background = brush;
        StatusBar.Background = brush;
    }
}