using System.Reflection;
using A6ToolKits.Common.Action;
using A6ToolKits.Common.Attributes.Layout;
using A6ToolKits.Common.Attributes.MVVM;
using A6ToolKits.Helper.Assembly;
using A6ToolKits.Layout.ControlGenerator.Interfaces;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
namespace A6ToolKits.Layout.Generator;

/// <summary>
///     工具栏生成器
/// </summary>
[AutoRegister(typeof(ToolBarGenerator), RegisterType.Singleton)]
public class ToolBarGenerator : IControlGenerator<StackPanel>
{

    /// <summary>
    ///     生成工具栏
    /// </summary>
    /// <returns>
    ///     生成的工具栏
    /// </returns>
    public StackPanel? Generate()
    {
        var result = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            Name = "ToolBar"
        };

        var types = AssemblyHelper.GetTypeWithAttribute<ToolBarActionAttribute>();
        var groups = types.GroupBy(t => t.GetCustomAttribute<ToolBarActionAttribute>()?.Group).OrderBy(group => group.Key).ToList();
        
        if (groups.Count == 0) return null;

        foreach (var group in groups)
        {
            foreach (var type in group)
            {
                var obj = WindowGenerator.Creator?.CreateInstance(type);
                if (obj is not ActionBase action) continue;
                var buttonType = action.Icon == null ? ButtonType.Initials : ButtonType.Icon;
                var button = action.GenerateButton(buttonType);
                button.Height = WindowConfig.TopBarHeight - 5;
                button.Margin = new Thickness(3, 1, 0, 1);
                result.Children.Add(button);
            }

            result.Children[^1].Margin = new Thickness(3, 1);

            result.Children.Add(new Separator
            {
                Width = 1,
                Height = double.NaN,
                Margin = new Thickness(1, 2)
            });
        }
        
        if (result.Children.Count > 0)
            result.Children.RemoveAt(result.Children.Count - 1);
        return result;
    }
}