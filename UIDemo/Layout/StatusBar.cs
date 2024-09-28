using A6ToolKits.Common.Attributes.Layout;
using A6ToolKits.Layout.Definer;
using Avalonia.Controls;

namespace UIDemo.Layout;

public class StatusBar : StatusBarDefiner
{
    [StatusBar("Left")]
    public TextBlock Left { get; } = new()
    {
        Text = "欢迎使用 A6ToolKits 框架"
    };
    
    [StatusBar("Right")]
    public TextBlock Right { get; } = new()
    {
        Text = "这是右边的状态栏"
    };
}