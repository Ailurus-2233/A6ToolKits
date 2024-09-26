using System.Reflection;
using A6ToolKits.Common.Attributes.Layout;
using A6ToolKits.Layout.Definer.Interfaces;
using Avalonia;
using Avalonia.Controls;

namespace A6ToolKits.Layout.Definer;

public abstract class StatusBarDefiner : IDefiner<DockPanel>
{
    public DockPanel Build()
    {
        var result = new DockPanel();
        var left = GenerateStatusBar(StatusPosition.Left);
        var center = GenerateStatusBar(StatusPosition.Center);
        var right = GenerateStatusBar(StatusPosition.Right);

        DockPanel.SetDock(left, Dock.Left);
        DockPanel.SetDock(right, Dock.Right);

        result.Children.Add(left);
        result.Children.Add(right);
        result.Children.Add(center);

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

        result.Children.RemoveAt(result.Children.Count - 1);
        return result;
    }
}