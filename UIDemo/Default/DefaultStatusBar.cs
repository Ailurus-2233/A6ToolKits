using A6ToolKits.Common.Attributes.Layout;
using A6ToolKits.Layout.Definitions;
using Avalonia.Controls;

namespace UIDemo.Default;


public class DefaultStatusBar : IDefiner
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