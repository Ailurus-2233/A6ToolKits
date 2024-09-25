using A6ToolKits.UIPackage.Controls.Layout.Tab.Models;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using TabItem = Avalonia.Controls.TabItem;

namespace A6ToolKits.UIPackage.Controls.Layout.Tab;

public class TabHeader : TemplatedControl
{
    public static readonly StyledProperty<TabCollection> TabCollectionProperty =
        AvaloniaProperty.Register<TabHeader, TabCollection>(nameof(TabCollection));

    public static readonly StyledProperty<Orientation> OrientationProperty =
        AvaloniaProperty.Register<TabHeader, Orientation>(nameof(Orientation));

    public static readonly StyledProperty<double> IconSizeProperty =
        AvaloniaProperty.Register<TabHeader, double>(nameof(IconSize), 20);

    public static readonly StyledProperty<string> PromptLinePositionProperty =
        AvaloniaProperty.Register<TabHeader, string>(nameof(PromptLinePosition), "Bottom");

    public static readonly StyledProperty<string> DisplayTypeProperty =
        AvaloniaProperty.Register<TabHeader, string>(nameof(DisplayType), "Text");
    
    public static readonly StyledProperty<bool> IsCloseableProperty =
        AvaloniaProperty.Register<TabHeader, bool>(nameof(IsCloseable), true);

    public TabCollection TabCollection
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

    public string PromptLinePosition
    {
        get => GetValue(PromptLinePositionProperty);
        set => SetValue(PromptLinePositionProperty, value);
    }

    public string DisplayType
    {
        get => GetValue(DisplayTypeProperty);
        set => SetValue(DisplayTypeProperty, value);
    }
    
    public bool IsCloseable
    {
        get => GetValue(IsCloseableProperty);
        set => SetValue(IsCloseableProperty, value);
    }
}