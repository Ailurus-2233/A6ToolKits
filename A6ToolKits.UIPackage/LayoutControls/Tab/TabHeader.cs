using A6ToolKits.UIPackage.LayoutControls.Tab.Models;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;

namespace A6ToolKits.UIPackage.LayoutControls.Tab;

public class TabHeader : TemplatedControl
{
    public static readonly StyledProperty<TabItemCollection> TabCollectionProperty =
        AvaloniaProperty.Register<TabHeader, TabItemCollection>(nameof(TabCollection));

    public static readonly StyledProperty<Orientation> OrientationProperty =
        AvaloniaProperty.Register<TabHeader, Orientation>(nameof(Orientation), Orientation.Horizontal);

    public static readonly StyledProperty<double> IconSizeProperty =
        AvaloniaProperty.Register<TabHeader, double>(nameof(IconSize), 20);

    public TabItemCollection TabCollection
    {
        get => GetValue(TabCollectionProperty);
        set => SetValue(TabCollectionProperty, value);
    }

    public Orientation Orientation
    {
        get => GetValue(OrientationProperty);
        set => SetValue(OrientationProperty, value);
    }

    public double IconSize
    {
        get => GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
    }
}