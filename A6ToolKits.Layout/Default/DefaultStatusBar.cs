using A6ToolKits.Layout.Attributes;
using A6ToolKits.Layout.Definitions;
using Avalonia.Controls;

namespace A6ToolKits.Layout.Default;

public class DefaultStatusBar : IDefinition
{
    [StatusBar("Left")]
    public TextBlock Left { get; } = new TextBlock()
    {
        Text = "欢迎使用 A6ToolKits 框架"
    };

    [StatusBar("Center")]
    public TextBlock Center { get; } = new TextBlock()
    {
        Text = "这是中间的状态栏"
    };

    [StatusBar("Right")]
    public TextBlock Right { get; } = new TextBlock()
    {
        Text = "这是右边的状态栏"
    };
}