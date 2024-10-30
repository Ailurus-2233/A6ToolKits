using System.Reflection;
using A6ToolKits.Action;
using A6ToolKits.Assembly;
using A6ToolKits.Common.Attributes.Layout;
using A6ToolKits.Layout.Generator;
using Avalonia;
using Avalonia.Controls;

namespace A6ToolKits.Layout.Helper;

/// <summary>
///     工具栏生成工具
/// </summary>
internal static class ToolBarGenerateHelper
{
    private static WindowConfig _config { get; set; } = WindowConfig.Instance;
    private static WindowGenerator _generator { get; set; } = WindowGenerator.Instance;

    /// <summary>
    ///     获取所有有ToolBar属性的ActionBase类
    /// </summary>
    public static List<IGrouping<int?, Type>> GetToolBarGroup()
    {
        var types = AssemblyHelper.GetTypeWithAttribute<ToolBarActionAttribute>();
        var groups = types.GroupBy(t => t.GetCustomAttribute<ToolBarActionAttribute>()?.Group)
            .OrderBy(group => group.Key).ToList();
        return groups;
    }

    /// <summary>
    ///     根据类型生成一组按钮
    /// </summary>
    /// <param name="types">
    ///     一些继承了ActionBase的类
    /// </param>
    /// <returns>
    ///     按钮列表
    /// </returns>
    public static List<Button> GenerateButtons(List<Type> types)
    {
        var result = new List<Button>();
        types.ForEach(type =>
        {
            var obj = _generator.Creator?.Create(type);
            if (obj is not ActionBase action) return;
            var buttonType = action.Icon == null ? ButtonType.Initials : ButtonType.Icon;
            var button = action.GenerateButton(buttonType);
            result.Add(button);
        });
        return result;
    }

    /// <summary>
    ///     生成工具栏中的所有子控件 按钮+分割线
    /// </summary>
    /// <returns>
    ///     子控件列表
    /// </returns>
    public static List<Control> GenerateToolBarItems()
    {
        var result = new List<Control>();
        var groups = GetToolBarGroup();
        if (groups.Count == 0) return result;
        groups.ForEach(group =>
        {
            var buttons = GenerateButtons(group.ToList());
            result.AddRange(buttons);
            result.Add(new Separator()
            {
                Width = 1,
                Height = double.NaN,
            });
        });

        if (result.Count > 0)
            result.RemoveAt(result.Count - 1);
        return result;
    }
}