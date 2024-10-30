using A6ToolKits.Action;
using A6ToolKits.Resource;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;

namespace A6ToolKits.Layout.Controls.WindowActions;

/// <summary>
///     关闭程序动作
/// </summary>
public class CloseAction : ActionBase
{
    /// <inheritdoc />
    public override string? Name { get; init; } = "关闭程序";

    /// <inheritdoc />
    public override string? ToolTip { get; init; } = "关闭程序";

    /// <inheritdoc />
    public override IImage? Icon { get; init; } = ResourceHelper.LoadImage("CloseIcon");

    /// <inheritdoc />
    public override Task Run()
    {
        var controller = CoreService.Instance.Controller;
        controller?.StopApplication();
        return Task.CompletedTask;
    }
}