using A6ToolKits.UIPackage.Controls.Layout.Tab.Models;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace A6ToolKits.UIPackage.Controls.Layout.Tab;

public class TabContainer : TemplatedControl
{
    public static readonly StyledProperty<TabItemCollection> TabCollectionProperty =
        AvaloniaProperty.Register<TabHeader, TabItemCollection>(nameof(TabCollection));

    public static readonly DirectProperty<TabContainer, ContentControl> DisplayControlProperty =
        AvaloniaProperty.RegisterDirect<TabContainer, ContentControl>(
            nameof(DisplayControl),
            o => o.TabCollection.SelectedItem is { Content: not null }
                ? o.TabCollection.SelectedItem.Content
                : new ContentControl());
    
    public TabItemCollection TabCollection
    {
        get => GetValue(TabCollectionProperty);
        set => SetValue(TabCollectionProperty, value);
    }

    public ContentControl DisplayControl
    {
        get => GetValue(DisplayControlProperty);
        set => SetValue(DisplayControlProperty, value);
    }

    public TabContainer()
    {
        TabCollection.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName != nameof(TabItemCollection.SelectedItem)) return;
            var newValue = TabCollection.SelectedItem is { Content: not null }
                ? TabCollection.SelectedItem.Content
                : new ContentControl();
            RaisePropertyChanged(DisplayControlProperty, DisplayControl, newValue);
        };
    }
}