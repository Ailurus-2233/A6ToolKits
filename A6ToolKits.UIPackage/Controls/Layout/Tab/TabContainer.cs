using A6ToolKits.UIPackage.Controls.Layout.Tab.Models;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace A6ToolKits.UIPackage.Controls.Layout.Tab;

public class TabContainer : TemplatedControl
{
    public static readonly StyledProperty<TabItemCollection> TabCollectionProperty =
        AvaloniaProperty.Register<TabHeader, TabItemCollection>(nameof(TabCollection));
    
    public TabItemCollection TabCollection
    {
        get => GetValue(TabCollectionProperty);
        set => SetValue(TabCollectionProperty, value);
    }
}