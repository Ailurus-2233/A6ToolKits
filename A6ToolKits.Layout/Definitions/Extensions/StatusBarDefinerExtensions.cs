using System.Reflection;
using A6ToolKits.Attributes;
using Avalonia;
using Avalonia.Controls;

namespace A6ToolKits.Layout.Definitions.Extensions;

/// <summary>
///     状态栏定义类扩展，用于生成状态栏
/// </summary>
public static class StatusBarDefinerExtensions
{
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
    public static List<Control> GenerateStatusBar(this IDefiner definer, StatusPosition position)
    {
        var result = new List<Control>();

        var properties = definer.GetType().GetProperties().Where(p =>
        {
            var attr = GetStatusBarAttribute(p);
            return attr != null && attr.Position == position;
        });

        foreach (var property in properties)
        {
            if (property.GetValue(definer) is not Control control) continue;
            result.Add(control);
            result.Add(new Separator
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