using A6ToolKits.Action;
using A6ToolKits.Layout.Helper.Interfaces;
using Avalonia.Controls;

namespace A6ToolKits.Layout.Helper;

/// <summary>
///     菜单生成器，用于生成菜单项
/// </summary>
public class MenuGenerateHelper : IControlGenerateHelper<MenuItem>
{
    /// <summary>
    ///     基于 ActionBase 生成一个菜单项，用于显示在菜单中
    /// </summary>
    /// <returns>
    ///     生成的菜单项
    /// </returns>
    public MenuItem GenerateControl(ActionBase action)
    {
        var menuItem = new MenuItem
        {
            Header = action.Name,
            IsEnabled = action.CanRun,
            Icon = action.Icon
        };

        menuItem.Click += (_, _) => action.Run();
        action.CanRunChanged += (_, _) => menuItem.IsEnabled = action.CanRun;

        return menuItem;
    }
}