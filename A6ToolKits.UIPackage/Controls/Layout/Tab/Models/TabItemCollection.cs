using System.Collections.ObjectModel;
using A6ToolKits.Layout.Controls;

namespace A6ToolKits.UIPackage.Controls.Layout.Tab.Models;

public class TabItemCollection(string groupName, ObservableCollection<TabItem> items) : ControlBase
{
    private TabItem? _selectedItem;
    private string _groupName = groupName;
    private ObservableCollection<TabItem> _items = items;

    public ObservableCollection<TabItem> Items
    {
        get => _items;
        set => SetField(ref _items, value);
    }

    public TabItem? SelectedItem
    {
        get => _selectedItem;
        set
        {
            if (_selectedItem != null)
                _selectedItem.IsSelected = false;
            if (value != null)
                value.IsSelected = true;
            SetField(ref _selectedItem, value);
        }
    }

    public string GroupName
    {
        get => _groupName;
        set => SetField(ref _groupName, value);
    }
}