using A6ToolKits.Action;
using A6ToolKits.Resource;
using Avalonia.Controls;
using Avalonia.Media;

namespace A6ToolKits.Layout.Controls.WindowActions;

/// <summary>
///     最小化动作
/// </summary>
public class MinimizeAction : ActionBase
{
    /// <inheritdoc />
    public override string? Name { get; init; } = "最小化";

    /// <inheritdoc />
    public override string? ToolTip { get; init; } = "最小化";

    /// <inheritdoc />
    public override IImage? Icon { get; init; } = ResourceHelper.LoadImage("MinusIcon");

    /// <inheritdoc />
    public override Task Run()
    {
        var window = CoreService.Instance.Controller?.GetMainWindow();
        if (window == null) return Task.CompletedTask;
        window.WindowState = WindowState.Minimized;
        return Task.CompletedTask;
    }
}