using System.Collections.ObjectModel;
using A6ToolKits.Layout.Controls;

namespace A6ToolKits.UIPackage.Controls.Layout.Tab.Models;

public class TabItemCollection : ControlBase
{
    private TabItem? _selectedItem;
    private string? _groupName;
    private ObservableCollection<TabItem> _items;
    private bool? _isCloseable;

    public ObservableCollection<TabItem> Items
    {
        get => _items;
        set => SetField(ref _items, value);
    }

    public string? GroupName
    {
        get => _groupName;
        set => SetField(ref _groupName, value);
    }

    public TabItem? SelectedItem
    {
        get
        {
            if (_selectedItem != null) return _selectedItem;
            if (Items.Count == 0) return null;
            _selectedItem = Items[0];
            _selectedItem.IsSelected = true;
            return _selectedItem;
        }
        set
        {
            if (_selectedItem != null)
                _selectedItem.IsSelected = false;
            if (value != null)
                value.IsSelected = true;
            SetField(ref _selectedItem, value);
        }
    }

    public bool? IsCloseable
    {
        get => _isCloseable;
        set => SetField(ref _isCloseable, value);
    }

    public TabItemCollection(string groupName, ObservableCollection<TabItem> items)
    {
        _groupName = groupName;
        _items = items;
        TabItem.Selected += (sender, s) =>
        {
            if (s == _groupName && sender is TabItem tabItem)
            {
                SelectedItem = tabItem;
            }
        };

        TabItem.Deleted += (sender, s) =>
        {
            if (s != _groupName || sender is not TabItem tabItem) return;
            var index = Items.IndexOf(tabItem);
            Items.RemoveAt(index);
            if (index == 0)
                SelectedItem = Items.Count > 0 ? Items[0] : null;
            else
                SelectedItem = Items[index - 1];
        };
    }
}