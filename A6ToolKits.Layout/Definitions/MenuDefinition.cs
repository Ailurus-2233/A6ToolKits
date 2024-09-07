using System.Reflection;
using A6ToolKits.Layout.Attributes;
using A6ToolKits.Layout.Generate;
using Avalonia.Controls;
using DynamicData;
using Serilog;

namespace A6ToolKits.Layout.Definitions;

public abstract class MenuDefinition : IDefinition
{
    public List<MenuItem> GenerateMenu()
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
            GenerateMenu(1, group).ForEach(item => { menuItem.Items.Add(item); });
            result.Add(menuItem);
        }

        return result;
    }

    private List<MenuItem> GenerateMenu(int index, IGrouping<string?, PropertyInfo> group)
    {
        var result = new List<MenuItem>();

        var properties = group.Where(property => GetMenuAttribute(property)?.Path.Length == index);
        var groups = group.Where(property => GetMenuAttribute(property)?.Path.Length > index)
            .GroupBy(property => GetMenuAttribute(property)?.Path[index].ItemName);

        foreach (var next in groups)
        {
            var menuItem = new MenuItem()
            {
                Header = next.Key,
            };
            GenerateMenu(index + 1, next).ForEach(item => { menuItem.Items.Add(item); });
            result.Add(menuItem);
        }

        foreach (var property in properties)
        {
            var attr = GetMenuAttribute(property);
            var temp = (property.GetValue(this) as MenuActionBase)?.GenerateControl();
            if (temp != null) result.Add(temp);
        }

        return result;
    }

    private static MenuAttribute? GetMenuAttribute(PropertyInfo property)
    {
        return property.GetCustomAttribute<MenuAttribute>();
    }
}