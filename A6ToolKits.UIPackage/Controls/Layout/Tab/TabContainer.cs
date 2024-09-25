using A6ToolKits.UIPackage.Controls.Layout.Tab.Models;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace A6ToolKits.UIPackage.Controls.Layout.Tab;

/// <summary>
///     页面容器，用于管理多个页面
/// </summary>
public class TabContainer : TemplatedControl
{
    public static readonly StyledProperty<Dock> TabStripPlacementProperty =
        AvaloniaProperty.Register<TabContainer, Dock>(nameof(TabStripPlacement), defaultValue: Dock.Top);

    public static readonly StyledProperty<TabCollection> TabCollectionProperty =
        AvaloniaProperty.Register<TabContainer, TabCollection>(nameof(TabCollection));

    public static readonly StyledProperty<bool> IsHeaderVisibleProperty =
        AvaloniaProperty.Register<TabContainer, bool>(nameof(IsHeaderVisible), defaultValue: true);
    
    public static readonly StyledProperty<string> DisplayTypeProperty =
        AvaloniaProperty.Register<TabContainer, string>(nameof(DisplayType), "Text");
    
    public static readonly StyledProperty<bool> IsHeaderCloseableProperty =
        AvaloniaProperty.Register<TabContainer, bool>(nameof(IsHeaderCloseable), defaultValue: true);
    
    public static readonly StyledProperty<double> HeaderHeightProperty =
        AvaloniaProperty.Register<TabContainer, double>(nameof(HeaderHeight), 30);
    
    public static readonly StyledProperty<double> HeaderWidthProperty =
        AvaloniaProperty.Register<TabContainer, double>(nameof(HeaderWidth), 30);
    
    public static readonly StyledProperty<int> HeaderFontSizeProperty =
        AvaloniaProperty.Register<TabContainer, int>(nameof(HeaderFontSize), 14);
    
    public static readonly StyledProperty<int> HeaderIconSizeProperty =
        AvaloniaProperty.Register<TabContainer, int>(nameof(HeaderIconSize), 20);

    public Dock TabStripPlacement
    {
        get => GetValue(TabStripPlacementProperty);
        set => SetValue(TabStripPlacementProperty, value);
    }

    public TabCollection TabCollection
    {
        get => GetValue(TabCollectionProperty);
        set => SetValue(TabCollectionProperty, value);
    }

    public bool IsHeaderVisible
    {
        get => GetValue(IsHeaderVisibleProperty);
        set => SetValue(IsHeaderVisibleProperty, value);
    }
    
    public string DisplayType
    {
        get => GetValue(DisplayTypeProperty);
        set => SetValue(DisplayTypeProperty, value);
    }
    
    public bool IsHeaderCloseable
    {
        get => GetValue(IsHeaderCloseableProperty);
        set => SetValue(IsHeaderCloseableProperty, value);
    }
    
    public double HeaderHeight
    {
        get => GetValue(HeaderHeightProperty);
        set => SetValue(HeaderHeightProperty, value);
    }
    
    public double HeaderWidth
    {
        get => GetValue(HeaderWidthProperty);
        set => SetValue(HeaderWidthProperty, value);
    }
    
    public int HeaderFontSize
    {
        get => GetValue(HeaderFontSizeProperty);
        set => SetValue(HeaderFontSizeProperty, value);
    }
    
    public int HeaderIconSize
    {
        get => GetValue(HeaderIconSizeProperty);
        set => SetValue(HeaderIconSizeProperty, value);
    }
    
}