using A6ToolKits.UIPackage.Controls.TabControl.Models;
using Avalonia;

namespace A6ToolKits.UIPackage.Controls.TabControl;

public class TabBody : ControlBase
{
    public static readonly StyledProperty<TabCollection> TabCollectionProperty =
        AvaloniaProperty.Register<TabHeader, TabCollection>(nameof(TabCollection));

    public TabCollection TabCollection
    {
        get => GetValue(TabCollectionProperty);
        set => SetValue(TabCollectionProperty, value);
    }
}