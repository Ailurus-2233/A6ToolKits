using A6ToolKits.ApplicationController;
using A6ToolKits.Container;
using A6ToolKits.Controls.Command;
using A6ToolKits.ResourceLoader;
using Avalonia.Controls;
using Avalonia.Media;

namespace A6ToolKits.Layout.Controls.ControlCommand;

/// <summary>
///     最大化 Command
/// </summary>
public sealed class MaximizeCommand : CommandBase
{
    /// <inheritdoc />
    public override string Text => "最大化";

    /// <inheritdoc />
    public override string ToolTip => "最大化";

    /// <inheritdoc />
    public override IImage? Image { get; } = ResourceHelper.LoadImage("MaximizeIcon");

    public override Task Run()
    {
        var window = IoC.GetInstance<IApplicationController>()?.MainWindow;
        if (window is null) return Task.CompletedTask;
        window.WindowState = WindowState.Maximized;
        return Task.CompletedTask;
    }
}