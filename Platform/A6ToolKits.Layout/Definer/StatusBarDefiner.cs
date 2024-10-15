using System.Reflection;
using A6ToolKits.Common.Attributes.Layout;
using A6ToolKits.Layout.Definer.Interfaces;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

namespace A6ToolKits.Layout.Definer;

public abstract class StatusBarDefiner : IDefiner<DockPanel>
{
    public DockPanel Build()
    {
        var result = new DockPanel();
        var left = GenerateStatusBar(StatusPosition.Left);
        var right = GenerateStatusBar(StatusPosition.Right);

        DockPanel.SetDock(left, Dock.Left);
        DockPanel.SetDock(right, Dock.Right);

        result.Children.Add(left);
        result.Children.Add(right);
        
        return result;
    }

    /// <summary>
    ///     根据属性中的 StatusBarAttribute 生成菜单项
    /// </summary>
    /// <param name="definer">
    ///     定义类
    /// </param>
    /// <param name="position">
    ///     指定位置
    /// </param>
    /// <returns>
    ///     生成的状态栏控件列表，需要将其添加到对应的状态栏中
    /// </returns>
    public StackPanel GenerateStatusBar(StatusPosition position)
    {
        var result = new StackPanel();

        var properties = GetType().GetProperties().Where(p =>
        {
            var attr = p.GetCustomAttribute<StatusBarAttribute>();
            return attr != null && attr.Position == position;
        });

        foreach (var property in properties)
        {
            if (property.GetValue(this) is not Control control) continue;
            result.Children.Add(control);
            result.Children.Add(new Separator
            {
                Width = 1,
                Height = double.NaN,
                Margin = new Thickness(5)
            });
        }

        switch (position)
        {
            case StatusPosition.Left:
                result.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Left);
                result.Margin = new Thickness(8, 0, 0, 0);
                break;
            case StatusPosition.Right:
                result.SetValue(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Right);
                result.Margin = new Thickness(0, 0, 8, 0);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(position), position, null);
        }
        
        result.SetValue(Layoutable.VerticalAlignmentProperty, VerticalAlignment.Center);
        
        result.Children.RemoveAt(result.Children.Count - 1);
        return result;
    }
}