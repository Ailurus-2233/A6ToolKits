using A6ToolKits.Bootstrapper;
using A6ToolKits.Command;
using A6ToolKits.Common.Container;
using A6ToolKits.Common.ResourceLoader;
using Avalonia.Controls;
using Avalonia.Media;

namespace A6ToolKits.Layout.Controls.ControlCommand;

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
public sealed class MinimizeCommand : CommandBase
{
    public override string? Name { get; } = "最小化";
    public override string? ToolTip { get; } = "最小化";
    public override IImage? Image { get; } = ResourceHelper.LoadImage("LayoutMinusIcon");

    public override Task Run()
    {
        var window = IoC.GetInstance<IWindowController>()?.GetMainWindow();
        if (window == null) return Task.CompletedTask;
        window.WindowState = WindowState.Minimized;
        return Task.CompletedTask;
    }
}

#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释