using System.Reflection;
using A6ToolKits.Action;
using A6ToolKits.Layout.Attributes;
using A6ToolKits.Layout.Generator;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

namespace A6ToolKits.Layout.Definitions;

public static class ToolBarDefinitionExtensions
{
    public static List<Control> GenerateToolBar(this IDefinition definition, Position position)
    {
        var result = new List<Control>();

        var properties = definition.GetType().GetProperties().Where(p =>
        {
            var attr = GetToolBarAttribute(p);
            return attr != null && attr.Position == position;
        });

        var groups = properties.GroupBy(p => GetToolBarAttribute(p)?.Group);

        var buttonGenerator = new ButtonGenerator();

        foreach (var group in groups)
        {
            foreach (var property in group)
            {
                if (property.GetValue(definition) is not ActionBase action) continue;
                buttonGenerator.Type = action.Icon == null ? ButtonType.Initials : ButtonType.Icon;
                var button = buttonGenerator.GenerateControl(action);
                result.Add(button);
            }

            result.Add(new Separator()
            {
                Width = 1,
                Height = double.NaN,
                Margin = Thickness.Parse("5")
            });
        }

        result.RemoveAt(result.Count - 1);

        return result;
    }

    private static ToolBarAttribute? GetToolBarAttribute(PropertyInfo propertyInfo)
    {
        return propertyInfo.GetCustomAttribute<ToolBarAttribute>();
    }
}