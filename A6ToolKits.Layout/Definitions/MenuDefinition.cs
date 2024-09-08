using System.Reflection;
using A6ToolKits.Layout.Attributes;
using A6ToolKits.Layout.Generate;
using Avalonia.Controls;
using Avalonia.Remote.Protocol.Input;
using DynamicData;
using Serilog;

namespace A6ToolKits.Layout.Definitions;

public abstract class MenuDefinition : IDefinition<MenuItem>
{
    public List<MenuItem> Generate()
    {
        var properties = GetType().GetProperties().Where(p => GetMenuAttribute(p) != null);

        var result = new List<MenuItem>();

        var groups = properties.GroupBy(p => GetMenuAttribute(p)?.Path[0].ItemName);

        foreach (var group in groups)
        {
            var menuItem = new MenuItem()
            {
                Header = group.Key,
            };
            var dict = Generate(1, group);
            AddResult(menuItem, dict);
            result.Add(menuItem);
        }

        return result;
    }

    private Dictionary<int, List<MenuItem>> Generate(int index, IGrouping<string?, PropertyInfo> group)
    {
        var dict = new Dictionary<int, List<MenuItem>>();

        var properties = group.Where(property => GetMenuAttribute(property)?.Path.Length == index);
        var groups = group.Where(property => GetMenuAttribute(property)?.Path.Length > index)
            .GroupBy(property => GetMenuAttribute(property)?.Path[index].ItemName);

        foreach (var next in groups)
        {
            var menuItem = new MenuItem()
            {
                Header = next.Key,
            };

            var children = Generate(index + 1, next);

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

        foreach (var property in properties)
        {
            var temp = (property.GetValue(this) as MenuActionBase)?.GenerateControl();
            var key = GetMenuAttribute(property)?.Path[index - 1].Order;
            if (key == null || temp == null) continue;
            if (!dict.TryGetValue(key.Value, out var value))
            {
                value = new List<MenuItem>();
                dict[key.Value] = value;
            }

            value.Add(temp);
        }

        return dict;
    }

    private static MenuAttribute? GetMenuAttribute(PropertyInfo property)
    {
        return property.GetCustomAttribute<MenuAttribute>();
    }

    private void AddResult(MenuItem menuItem, Dictionary<int, List<MenuItem>> children)
    {
        var latest = new Separator();
        children.Keys.ToList().ForEach(key =>
        {
            children[key].ForEach(item => menuItem.Items.Add(item));
            latest = new Separator();
            menuItem.Items.Add(latest);
        });
        menuItem.Items.Remove(latest);
    }
}