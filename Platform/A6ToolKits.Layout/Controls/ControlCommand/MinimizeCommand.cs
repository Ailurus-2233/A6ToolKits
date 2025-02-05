using A6ToolKits.ApplicationController;
using A6ToolKits.Container;
using A6ToolKits.Controls.Command;
using A6ToolKits.Desktop.ResourceLoader;
using Avalonia.Controls;
using Avalonia.Media;

namespace A6ToolKits.Layout.Controls.ControlCommand;

/// <summary>
///     最小化 Command
/// </summary>
public sealed class MinimizeCommand : CommandBase
{
    /// <inheritdoc />
    public override string? Text => "最小化";

    /// <inheritdoc />
    public override string? ToolTip => "最小化";

    /// <inheritdoc />
    public override IImage? Image { get; } = ResourceHelper.LoadImage("MinusIcon");

    /// <inheritdoc />
    public override Task Run()
    {
        var window = IoC.GetInstance<IApplicationController>()?.MainWindow;
        if (window == null) return Task.CompletedTask;
        window.WindowState = WindowState.Minimized;
        return Task.CompletedTask;
    }
}