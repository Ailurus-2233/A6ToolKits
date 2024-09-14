using Avalonia.Controls;

namespace A6ToolKits.Layout.Container;

public class BasePageContainer : IContainer<BasePageItem>
{
    public List<BasePageItem> Items { get; set; }
    public IContainerItem SelectedItem { get; set; }

    public ContentControl GetTabsControl()
    {
        throw new NotImplementedException();
    }

    public void AddItem(BasePageItem item)
    {
        throw new NotImplementedException();
    }

    public void RemoveItem(BasePageItem item)
    {
        throw new NotImplementedException();
    }

    public void SelectItem(BasePageItem item)
    {
        throw new NotImplementedException();
    }
}