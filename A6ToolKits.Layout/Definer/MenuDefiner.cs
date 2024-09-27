using System.Reflection;
using A6ToolKits.Common.Action;
using A6ToolKits.Common.Attributes.Layout;
using A6ToolKits.Helper.Resource;
using A6ToolKits.Layout.Definer.Interfaces;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.MarkupExtensions;

namespace A6ToolKits.Layout.Definer;

public abstract class MenuDefiner : IDefiner<Menu>
{
    /// <summary>
    ///     根据属性中的 MenuAttribute 生成菜单项
    /// </summary>
    /// <returns>
    ///     根据自身生成的菜单
    /// </returns>
    public Menu Build()
    {
        var item = new MenuItem
        {
            Icon = ResourceHelper.LoadImage("MenuIcon")
        };
        var groups = GetMenuGroups();

        foreach (var group in groups)
        {
            var menuItem = new MenuItem
            {
                Header = group.Key
            };
            var dict = Generate(1, group);
            AddResult(menuItem, dict);
            item.Items.Add(menuItem);
        }

        var result = new Menu();
        result.Items.Add(item);
        
        return result;
    }

    private Dictionary<int, List<MenuItem>> Generate(int index, IGrouping<string?, PropertyInfo> group)
    {
        var dict = new Dictionary<int, List<MenuItem>>();

        var properties = group.Where(p => p.GetCustomAttribute<MenuAttribute>()?.Path.Length == index);
        var groups = group.Where(p => p.GetCustomAttribute<MenuAttribute>()?.Path.Length > index)
            .GroupBy(p => p.GetCustomAttribute<MenuAttribute>()?.Path[index].ItemName);

        foreach (var next in groups)
        {
            var menuItem = new MenuItem
            {
                Header = next.Key
            };

            var children = Generate(index + 1, next);
            AddResult(menuItem, children);

            var key = next.First().GetCustomAttribute<MenuAttribute>()?.Path[index - 1].Order;
            if (key == null) continue;
            AddKeyValue(key.Value, menuItem);
        }
        
        foreach (var property in properties)
        {
            if (property.GetValue(this) is not ActionBase temp) continue;
            var menuItem = temp.GenerateMenuItem();
            var key = property.GetCustomAttribute<MenuAttribute>()?.Path[index - 1].Order;
            if (key == null) continue;
            AddKeyValue(key.Value, menuItem);
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

    private IEnumerable<IGrouping<string, PropertyInfo>> GetMenuGroups()
    {
        var menuProperties = GetType().GetProperties().Where(p => p.GetCustomAttribute<MenuAttribute>() != null);
        return menuProperties.GroupBy(p => p.GetCustomAttribute<MenuAttribute>()!.Path[0].ItemName);
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
}