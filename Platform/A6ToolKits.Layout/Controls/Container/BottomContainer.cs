using A6ToolKits.Common.Attributes.MVVM;
using A6ToolKits.Layout.Generator;
using Avalonia.Controls;
namespace A6ToolKits.Layout.Controls.Container;

/// <summary>
///     底部容器
/// </summary>
[AutoRegister(typeof(TopContainer), RegisterType.Singleton)]
public class BottomContainer : UserControl
{
    private double StatusBarHeight  => WindowConfig.StatusBarHeight;

    private Window Window => WindowGenerator.Window;
    
    private StatusBarGenerator? StatusBarGenerator { get; set; }

    public BottomContainer()
    {
        StatusBarGenerator = WindowGenerator.Creator?.GetOrCreateInstance<StatusBarGenerator>();
    }
    
    public void GenerateBottomContainer()
    {
        Content = StatusBarGenerator?.Generate();
    }
}