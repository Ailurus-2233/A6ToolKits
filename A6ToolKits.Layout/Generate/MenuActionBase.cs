using A6ToolKits.Action;
using Avalonia.Controls;
using Avalonia.Styling;

namespace A6ToolKits.Layout.Generate;

public abstract class MenuActionBase : ActionBase, IGenerateControl<MenuItem>
{
    /// <summary>
    /// 基于 Name 生成一个菜单项，用于显示在菜单中
    /// </summary>
    /// <returns></returns>
    public MenuItem GenerateControl()
    {
        var menuItem = new MenuItem
        {
            Header = Name,
            IsEnabled = CanRun,
        };
        
        menuItem.Click += (_, _) => Run();
        CanRunChanged += (_, _) => menuItem.IsEnabled = CanRun;

        return menuItem;
    }
}