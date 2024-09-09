using A6ToolKits.Action;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

namespace A6ToolKits.Layout.Generator;

public class MenuGenerator : IControlGenerator<MenuItem>
{
    /// <summary>
    ///     基于 Name 生成一个菜单项，用于显示在菜单中
    /// </summary>
    /// <returns></returns>
    public MenuItem GenerateControl(ActionBase action)
    {
        var menuItem = new MenuItem
        {
            Header = action.Name,
            IsEnabled = action.CanRun,
            Icon = action.Icon,
        };

        menuItem.Click += (_, _) => action.Run();
        action.CanRunChanged += (_, _) => menuItem.IsEnabled = action.CanRun;

        return menuItem;
    }
}