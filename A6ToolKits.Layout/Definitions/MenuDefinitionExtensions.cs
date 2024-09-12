using System.Reflection;
using A6ToolKits.Action;
using A6ToolKits.Attributes;
using A6ToolKits.Layout.Generator;
using Avalonia.Controls;

namespace A6ToolKits.Layout.Definitions;

/// <summary>
///     菜单定义类扩展，用于生成菜单项
/// </summary>
public static class MenuDefinitionExtensions
{
    /// <summary>
    ///     根据属性中的 MenuAttribute 生成菜单项
    /// </summary>
    /// <param name="definition">
    ///     定义类
    /// </param>
    /// <returns>
    ///     菜单项列表，只需要将该列表添加到 Menu.Items 中即可
    /// </returns>
    public static List<MenuItem> GenerateMenuItem(this IDefinition definition)
    {
        var result = new List<MenuItem>();
        var properties = definition.GetType().GetProperties().Where(p => GetMenuAttribute(p) != null);
        var groups = properties.GroupBy(p => GetMenuAttribute(p)?.Path[0].ItemName);

        foreach (var group in groups)
        {
            var menuItem = new MenuItem
            {
                Header = group.Key
            };
            var dict = Generate(1, group, definition);
            AddResult(menuItem, dict);
            result.Add(menuItem);
        }

        return result;
    }

    private static Dictionary<int, List<MenuItem>> Generate(
        int index,
        IGrouping<string?, PropertyInfo> group,
        IDefinition definition)
    {
        var dict = new Dictionary<int, List<MenuItem>>();

        var properties = group.Where(property => GetMenuAttribute(property)?.Path.Length == index);
        var groups = group.Where(property => GetMenuAttribute(property)?.Path.Length > index)
            .GroupBy(property => GetMenuAttribute(property)?.Path[index].ItemName);

        foreach (var next in groups)
        {
            var menuItem = new MenuItem
            {
                Header = next.Key
            };

            var children = Generate(index + 1, next, definition);

            AddResult(menuItem, children);

            var key = GetMenuAttribute(next.First())?.Path[index - 1].Order;
            if (key == null) continue;
            if (!dict.TryGetValue(key.Value, out var value))
            {
                value = new List<MenuItem>();
                dict[key.Value] = value;
            }

            value.Add(menuItem);
        }

        var menuGenerator = new MenuGenerator();

        foreach (var property in properties)
        {
            if (property.GetValue(definition) is not ActionBase temp) continue;
            var menuItem = menuGenerator.GenerateControl(temp);
            var key = GetMenuAttribute(property)?.Path[index - 1].Order;
            if (key == null) continue;
            if (!dict.TryGetValue(key.Value, out var value))
            {
                value = new List<MenuItem>();
                dict[key.Value] = value;
            }

            value.Add(menuItem);
        }

        return dict;
    }

    private static MenuAttribute? GetMenuAttribute(PropertyInfo property)
    {
        return property.GetCustomAttribute<MenuAttribute>();
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