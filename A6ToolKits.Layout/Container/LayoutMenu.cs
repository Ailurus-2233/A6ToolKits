using A6ToolKits.Action;
using A6ToolKits.Action.Controls;
using Avalonia.Controls;
using Serilog;

namespace A6ToolKits.Layout.Container;

public abstract class LayoutMenu : LayoutItemBase
{
    public abstract List<LayoutMenuItem> MenuItems { get; set; }

    public List<MenuItem> GenerateMenu()
    {
        var result = new List<MenuItem>();

        MenuItems.ForEach(item =>
        {
            var menuItem = item.GenerateMenuItem();
            if (menuItem != null) result.Add(menuItem);
        });

        return result;
    }
}

public class LayoutMenuItem
{
    public bool IsGroup { get; set; } = false;
    public string GroupName { get; set; } = string.Empty;
    public bool IsEnabled { get; set; } = true;

    public ActionBase? Action { get; set; }

    public List<List<LayoutMenuItem>> SubItemGroups { get; set; } = [];

    public MenuItem? GenerateMenuItem()
    {
        var result = new ActionMenuItem();

        if (IsGroup)
        {
            result.Header = GroupName;
            SubItemGroups.ForEach(group =>
            {
                group.ForEach(item =>
                {
                    var subMenuItem = item.GenerateMenuItem();
                    if (subMenuItem != null)
                    {
                        result.Items.Add(subMenuItem);
                    }
                });
                result.Items.Add(new Separator());
            });
            result.PointerPressed += (_, _) =>
            {
                if (result.HasSubMenu)
                {
                    result.IsSubMenuOpen = true;
                }
            };
        }
        else
        {
            if (Action == null)
            {
                Log.Error("{0} MenuItem not contain Action", typeof(LayoutMenuItem));
                return null;
            }

            result.Action = Action;
        }

        result.IsEnabled = IsEnabled;

        return result;
    }
}