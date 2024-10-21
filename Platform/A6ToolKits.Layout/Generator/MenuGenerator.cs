using System.Reflection;
using A6ToolKits.Common.Action;
using A6ToolKits.Common.Attributes.Layout;
using A6ToolKits.Common.Attributes.MVVM;
using A6ToolKits.Common.Exceptions;
using A6ToolKits.Helper.Assembly;
using A6ToolKits.Helper.Resource;
using A6ToolKits.Layout.ControlGenerator.Enums;
using A6ToolKits.Layout.ControlGenerator.Interfaces;
using Avalonia;
using Avalonia.Controls;
namespace A6ToolKits.Layout.Generator;

/// <summary>
///     菜单生成器
/// </summary>
[AutoRegister(typeof(MenuGenerator), RegisterType.Singleton)]
public class MenuGenerator : IControlGenerator<Menu>
{
    /// <summary>
    ///     基于指定的菜单项生成菜单
    /// </summary>
    /// <returns>
    ///     生成的菜单控件
    /// </returns>
    public Menu? Generate()
    {
        var menuList = new List<MenuItem>();
        var groups = AssemblyHelper.GetTypeWithAttribute<MenuActionAttribute>()
            .GroupBy(t => GetMenuActionAttribute(t).Path[0].ItemName)
            .OrderBy(group => GetMenuActionAttribute(group.First()).Path[0].Order).ToList();

        if (groups.Count == 0) return null;

        foreach (var group in groups)
        {
            var menuItem = new MenuItem
            {
                Header = group.Key,
            };
            var dict = Generate(1, group);
            AddResult(menuItem, dict);
            menuList.Add(menuItem);
        }

        var result = new Menu
        {
            Height = WindowConfig.MenuHeight,
            Margin = new Thickness(5, 0),
            Name = "Menu"
        };
        switch (WindowConfig.MenuType)
        {
            case MenuType.Icon: {
                var item = new MenuItem
                {
                    Header = new Image
                    {
                        Source = ResourceHelper.LoadImage("MenuIcon"),
                        Height = WindowConfig.MenuHeight - 8,
                        Width = WindowConfig.MenuHeight - 8,
                    },
                    Padding = new Thickness(0),
                };
                menuList.ForEach(menuItem => item.Items.Add(menuItem));
                result.Items.Add(item);
                break;
            }
            case MenuType.Row:
                menuList.ForEach(menuItem => result.Items.Add(menuItem));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return result;
    }

    private Dictionary<int, List<MenuItem>> Generate(int index, IGrouping<string?, Type> group)
    {
        var dict = new Dictionary<int, List<MenuItem>>();

        var types = group.Where(type => GetMenuActionAttribute(type).Path.Length == index);
        var groups = group.Where(type => GetMenuActionAttribute(type).Path.Length > index)
            .GroupBy(type => GetMenuActionAttribute(type).Path[index].ItemName);

        foreach (var next in groups)
        {
            var menuItem = new MenuItem
            {
                Header = next.Key
            };

            var children = Generate(index + 1, next);
            AddResult(menuItem, children);

            var key = GetMenuActionAttribute(next.First()).Path[index - 1].Order;
            AddKeyValue(key, menuItem);
        }

        foreach (var type in types)
        {
            if (!typeof(ActionBase).IsAssignableFrom(type)) continue;
            var obj = WindowGenerator.Creator?.CreateInstance(type);
            if (obj is not ActionBase temp) continue;
            var menuItem = temp.GenerateMenuItem();
            var key = GetMenuActionAttribute(type).Path[index - 1].Order;
            AddKeyValue(key, menuItem);
        }

        return dict;

        void AddKeyValue(int key, MenuItem value)
        {
            if (!dict.TryGetValue(key, out var list))
            {
                list = [];
                dict[key] = list;
            }
            list.Add(value);
        }
    }
    private static void AddResult(MenuItem menuItem, Dictionary<int, List<MenuItem>> children)
    {
        children.Keys.ToList().ForEach(key =>
        {
            children[key].ForEach(item => menuItem.Items.Add(item));
            menuItem.Items.Add(new Separator());
        });
        menuItem.Items.RemoveAt(menuItem.Items.Count - 1);
    }

    private static MenuActionAttribute GetMenuActionAttribute(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);
        var attribute = type.GetCustomAttribute<MenuActionAttribute>();
        if (attribute == null) throw new TypeNotFoundException("Not found MenuActionAttribute in type");
        return attribute;
    }
}