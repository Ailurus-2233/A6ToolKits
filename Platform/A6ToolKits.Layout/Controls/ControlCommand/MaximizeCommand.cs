using A6ToolKits.Commands;
using A6ToolKits.Event;
using A6ToolKits.Resource;
using Avalonia.Controls;
using Avalonia.Media;

namespace A6ToolKits.Layout.Controls.ControlCommand;

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
public sealed class MaximizeCommand : CommandBase
{
    /// <inheritdoc />
    public override string? Name { get; } = "最大化";

    /// <inheritdoc />
    public override string? ToolTip { get; } = "最大化";
    
    public override IImage Image { get; } = ResourceHelper.LoadImage("MaximizeIcon");
    public override Task Run()
    {
        var window = CoreService.Instance.Controller?.GetMainWindow();
        if (window is null) return Task.CompletedTask;
        window.WindowState = WindowState.Maximized;
        return Task.CompletedTask;
    }
}

#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释