using System.Reflection;
using A6ToolKits.Common.Attributes.Layout;
using A6ToolKits.Common.Attributes.MVVM;
using A6ToolKits.Helper.Assembly;
using A6ToolKits.Layout.ControlGenerator.Interfaces;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
namespace A6ToolKits.Layout.Generator;

/// <summary>
///     状态栏生成器
/// </summary>
[AutoRegister(typeof(StatusBarGenerator), RegisterType.Singleton)]
public class StatusBarGenerator: IControlGenerator<DockPanel>
{

    /// <summary>
    ///     生成状态栏
    /// </summary>
    /// <returns>
    ///     生成的状态栏
    /// </returns>
    public DockPanel Generate()
    {
        var result = new DockPanel
        {
            Height = WindowConfig.StatusBarHeight
        };
        var left = GenerateStatusBar(StatusPosition.Left);
        var right = GenerateStatusBar(StatusPosition.Right);

        DockPanel.SetDock(left, Dock.Left);
        DockPanel.SetDock(right, Dock.Right);

        result.Children.Add(left);
        result.Children.Add(right);
        
        return result;
    }
    
    private static StackPanel GenerateStatusBar(StatusPosition position)
    {
        var result = new StackPanel();
        
        var allStatusBarTypes = AssemblyHelper.GetTypeWithAttribute<StatusBarAttribute>();
        var types = allStatusBarTypes.Where(t => t.GetCustomAttribute<StatusBarAttribute>()?.Position == position);
        
        foreach (var type in types)
        {
            var obj = WindowGenerator.Creator?.CreateInstance(type);
            if (obj is not Control control) continue;
            result.Children.Add(control);
        }

        switch (position)
        {
            case StatusPosition.Left:
                result.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Left);
                result.Margin = new Thickness(3, 0, 0, 0);
                break;
            case StatusPosition.Right:
                result.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Right);
                result.Margin = new Thickness(0, 0, 3, 0);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(position), position, null);
        }
        
        result.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center);
        
        return result;
    }
}