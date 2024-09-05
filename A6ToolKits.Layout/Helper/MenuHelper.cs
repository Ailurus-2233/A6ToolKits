using A6ToolKits.Layout.ActionMenu;
using A6ToolKits.Layout.ActionMenu.Controls;
using A6ToolKits.Layout.Default;
using Avalonia.Controls;

namespace A6ToolKits.Layout.Helper;

public static class MenuHelper
{
    public static Menu GenerateMenu()
    {
        var menu = new Menu();
        var demoMenu = new DemoMenu();
        foreach (var menuAction in demoMenu.MenuActions)
        {
            menu.Items.Add(GenerateActionMenuItem(menuAction));
        }

        return menu;
    }

    public static ActionMenuItem GenerateActionMenuItem(MenuActionDefinition action)
    {
        var menuItem = new ActionMenuItem
        {
            Action = action
        };
        
        menuItem.Click += async (sender, args) => await action.Run();

        if (action.IsGroup)
        {
            menuItem.Classes.Add("Group");
        }

        foreach (var subCommand in action.SubCommands)
        {
            menuItem.Items.Add(GenerateActionMenuItem(subCommand));
        }

        return menuItem;
    }
}