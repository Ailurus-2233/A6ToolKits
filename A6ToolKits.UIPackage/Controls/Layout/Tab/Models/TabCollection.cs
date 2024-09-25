using System.Collections.ObjectModel;

namespace A6ToolKits.UIPackage.Controls.Layout.Tab.Models;

public class TabCollection : ControlBase
{
    private TabItem? _selectedItem;
    private string? _groupName;
    private ObservableCollection<TabItem> _items;
    
    public ObservableCollection<TabItem> Items
    {
        get => _items;
        set
        {
            UpdateGroupName(value, _groupName);
            SetField(ref _items, value);
        }
    }

    public string? GroupName
    {
        get => _groupName;
        set
        {
            if (string.IsNullOrEmpty(value)) return;
            SetField(ref _groupName, value);
            UpdateGroupName(Items, value);
        }
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
            if (value != null && !Items.Contains(value)) return;
            if (_selectedItem != null)
                _selectedItem.IsSelected = false;
            if (value != null) value.IsSelected = true;
            SetField(ref _selectedItem, value);
        }
    }
    
    public TabCollection(ObservableCollection<TabItem>? items = null, string? groupName = null)
    {
        groupName ??= Guid.NewGuid().ToString();
        items ??= [];

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

        UpdateGroupName(items, groupName);
    }

    private static void UpdateGroupName(IList<TabItem> items, string groupName)
    {
        foreach (var item in items)
        {
            item.GroupName = groupName;
        }
    }

    public void AddItem(TabItem item)
    {
        item.GroupName = _groupName;
        Items.Add(item);
        SelectedItem ??= item;
    }
}