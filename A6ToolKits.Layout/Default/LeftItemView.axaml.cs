using A6ToolKits.Module;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace A6ToolKits.Layout.Default;

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
public partial class LeftItemView : UserControl
{
    public LeftItemView()
    {
        InitializeComponent();
    }

    public void OnLoaded(object sender, RoutedEventArgs e)
    {
        // Do something
        if (!ModuleLoader.TryGetModule<LayoutModule>(out var layoutModule)) return;
        var color = layoutModule?.WindowLayout?.MainColor;
        if (color != null) Background = new SolidColorBrush(color.Value);
    }
}
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释