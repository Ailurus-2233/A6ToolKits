using A6ToolKits.ApplicationController;
using A6ToolKits.Container;
using A6ToolKits.Controls.Command;
using A6ToolKits.Desktop.ResourceLoader;
using Avalonia.Controls;
using Avalonia.Media;

namespace A6ToolKits.Layout.Controls.ControlCommand;

/// <summary>
///    窗口化 Command
/// </summary>
public sealed class WindowCommand : CommandBase
{
    /// <inheritdoc />
    public override string? Text => "窗口化";

    /// <inheritdoc />
    public override string? ToolTip => "窗口化";

    /// <inheritdoc />
    public override IImage? Image { get; } = ResourceHelper.LoadImage("WindowIcon");

    /// <inheritdoc />
    public override Task Run()
    {
        var window = IoC.GetInstance<IApplicationController>()?.MainWindow;
        if (window is null) return Task.CompletedTask;
        window.WindowState = WindowState.Normal;
        return Task.CompletedTask;
    }
}
