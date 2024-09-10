using A6ToolKits.Layout.Attributes;
using A6ToolKits.Layout.Definitions;
using Avalonia.Controls;

namespace A6ToolKits.Layout.Default;

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

public class DefaultStatusBar : IDefinition
{
    [StatusBar("Left")]
    public TextBlock Left { get; } = new()
    {
        Text = "欢迎使用 A6ToolKits 框架"
    };

    [StatusBar("Center")]
    public TextBlock Center { get; } = new()
    {
        Text = "这是中间的状态栏"
    };

    [StatusBar("Right")]
    public TextBlock Right { get; } = new()
    {
        Text = "这是右边的状态栏"
    };
}

#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释