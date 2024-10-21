using System.Reflection;
using A6ToolKits.Common.Action;
using A6ToolKits.Common.Attributes.Layout;
using A6ToolKits.Layout.Definer.Interfaces;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using DynamicData;

namespace A6ToolKits.Layout.Definer;

public abstract class ToolBarDefiner : IDefiner<StackPanel>
{
    
    public StackPanel Build()
    {
        var result = new StackPanel
        {
            Orientation = Orientation.Horizontal
        };
        var properties = GetType().GetProperties().Where(p => p.GetCustomAttribute<ToolBarActionAttribute>() != null);
        var groups = properties.GroupBy(p => p.GetCustomAttribute<ToolBarActionAttribute>()?.Group);

        foreach (var group in groups)
        {
            foreach (var property in group)
            {
                if (property.GetValue(this) is not ActionBase action) continue;
                var type = action.Icon == null ? ButtonType.Initials : ButtonType.Icon;
                var button = action.GenerateButton(type);
                button.Margin = new Thickness(3, 3, 0, 3);
                result.Children.Add(button);
            }
            
            result.Children[^1].Margin = new Thickness(3);
            
            result.Children.Add(new Separator
            {
                Width = 1,
                Height = double.NaN,
                Margin = new Thickness(1, 5)
            });
        }
        result.Children.RemoveAt(result.Children.Count - 1);
        return result;
    }
}