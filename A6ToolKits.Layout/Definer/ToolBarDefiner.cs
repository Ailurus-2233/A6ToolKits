using System.Reflection;
using A6ToolKits.Common.Action;
using A6ToolKits.Common.Attributes.Layout;
using A6ToolKits.Layout.Definer.Interfaces;
using Avalonia;
using Avalonia.Controls;
using DynamicData;

namespace A6ToolKits.Layout.Definer;

public abstract class ToolBarDefiner : IDefiner<StackPanel>
{
    
    public StackPanel Build()
    {
        var result = new StackPanel();

        var properties = GetType().GetProperties().Where(p => p.GetCustomAttribute<ToolBarAttribute>() != null);
        var groups = properties.GroupBy(p => p.GetCustomAttribute<ToolBarAttribute>()?.Group);

        foreach (var group in groups)
        {
            foreach (var property in group)
            {
                if (property.GetValue(this) is not ActionBase action) continue;
                var type = action.Icon == null ? ButtonType.Initials : ButtonType.Icon;
                var button = action.GenerateButton(type);
                result.Children.Add(button);
            }

            result.Children.Add(new Separator
            {
                Width = 1,
                Height = double.NaN,
                Margin = new Thickness(5)
            });
        }
        result.Children.RemoveAt(result.Children.Count - 1);
        return result;
    }
}