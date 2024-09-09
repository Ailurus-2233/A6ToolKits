using System.Reflection;
using A6ToolKits.Action;
using A6ToolKits.Layout.Attributes;
using A6ToolKits.Layout.Generator;
using Avalonia;
using Avalonia.Controls;

namespace A6ToolKits.Layout.Definitions;

public static class StatusBarDefinitionExtensions
{
    public static List<Control> GenerateStatusBar(this IDefinition definition, StatusPosition position)
    {
        var result = new List<Control>();

        var properties = definition.GetType().GetProperties().Where(p =>
        {
            var attr = GetStatusBarAttribute(p);
            return attr != null && attr.Position == position;
        });

        foreach (var property in properties)
        {
            if (property.GetValue(definition) is not Control control) continue;
            result.Add(control);
            result.Add(new Separator()
            {
                Width = 1,
                Height = double.NaN,
                Margin = new Thickness(5)
            });
        }

        result.RemoveAt(result.Count - 1);
        return result;
    }

    private static StatusBarAttribute? GetStatusBarAttribute(PropertyInfo propertyInfo)
    {
        return propertyInfo.GetCustomAttribute<StatusBarAttribute>();
    }
}