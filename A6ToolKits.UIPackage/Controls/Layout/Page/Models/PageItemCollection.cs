using System.Collections.ObjectModel;

namespace A6ToolKits.UIPackage.Controls.Layout.Page.Models;

public class PageItemCollection : ControlBase
{
    private PageItem? _selectedItem;
    private ObservableCollection<PageItem> _items;
    
    public ObservableCollection<PageItem> Items
    {
        get => _items;
        set => SetField(ref _items, value);
    }
    
    public PageItem? SelectedItem
    {
        get
        {
            if (_selectedItem != null) return _selectedItem;
            if (Items.Count == 0) return null;
            _selectedItem = Items[0];
            _selectedItem.IsActivate = true;
            return _selectedItem;
        }
        set
        {
            if (_selectedItem != null)
                _selectedItem.IsActivate = false;
            if (value != null)
                value.IsActivate = true;
            SetField(ref _selectedItem, value);
        }
    }
    
    public PageItemCollection(ObservableCollection<PageItem> items)
    {
        _items = items;
        PageItem.Selected += (sender, _) =>
        {
            if (sender is PageItem pageItem)
                SelectedItem = pageItem;
        };
        
        PageItem.Deleted += (sender, _) =>
        {
            if (sender is not PageItem pageItem) return;
            var index = Items.IndexOf(pageItem);
            Items.Remove(pageItem);
            if (index == Items.Count)
                SelectedItem = Items.Count > 0 ? Items[0] : null;
            else
                SelectedItem = Items[index];
        };
    }
}