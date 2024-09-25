using A6ToolKits.UIPackage.Controls.Layout.Tab.Models;
using Avalonia;
using Avalonia.Controls.Primitives;

namespace A6ToolKits.UIPackage.Controls.Layout.Tab;

public class TabBody : TemplatedControl
{
    public static readonly StyledProperty<TabCollection> TabCollectionProperty =
        AvaloniaProperty.Register<TabHeader, TabCollection>(nameof(TabCollection));
    
    public TabCollection TabCollection
    {
        get => GetValue(TabCollectionProperty);
        set => SetValue(TabCollectionProperty, value);
    }
}