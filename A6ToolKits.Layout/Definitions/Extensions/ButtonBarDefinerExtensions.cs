using System.Reflection;
using A6ToolKits.Action;
using A6ToolKits.Attributes;
using A6ToolKits.Layout.Helper;
using Avalonia;
using Avalonia.Controls;

namespace A6ToolKits.Layout.Definitions.Extensions;

/// <summary>
///     工具栏定义类扩展，用于生成工具栏
/// </summary>
public static class ButtonBarDefinerExtensions
{
    /// <summary>
    ///     根据属性中的 ToolBarAttribute 生成工具栏
    /// </summary>
    /// <param name="definer">
    ///     定义类
    /// </param>
    /// <param name="buttonBarPosition">
    ///     工具栏位置
    /// </param>
    /// <returns>
    ///     生成的工具栏控件列表，需要将其添加到对应的工具栏中
    /// </returns>
    public static List<Control> GenerateButtonBar(this IDefiner definer, ButtonBarPosition buttonBarPosition)
    {
        var result = new List<Control>();

        var properties = definer.GetType().GetProperties().Where(p =>
        {
            var attr = GetToolBarAttribute(p);
            return attr != null && attr.Position == buttonBarPosition;
        });

        var groups = properties.GroupBy(p => GetToolBarAttribute(p)?.Group);

        var buttonGenerator = new ButtonGenerateHelper();

        foreach (var group in groups)
        {
            foreach (var property in group)
            {
                if (property.GetValue(definer) is not ActionBase action) continue;
                buttonGenerator.Type = action.Icon == null ? ButtonType.Initials : ButtonType.Icon;
                var button = buttonGenerator.GenerateControl(action);
                result.Add(button);
            }

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

    private static ToolBarAttribute? GetToolBarAttribute(PropertyInfo propertyInfo)
    {
        return propertyInfo.GetCustomAttribute<ToolBarAttribute>();
    }
}