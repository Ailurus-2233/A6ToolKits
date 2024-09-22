using System.Collections.ObjectModel;
using A6ToolKits.Layout.Controls;

namespace A6ToolKits.UIPackage.LayoutControls.Tab.Models;

public class TabItemCollection(string groupName, ObservableCollection<TabItem> items) : ControlBase
{
    private TabItem? _selectedItem;

    public ObservableCollection<TabItem> Items
    {
        get => items;
        set => SetField(ref items, value);
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
        get => groupName;
        set => SetField(ref groupName, value);
    }
}