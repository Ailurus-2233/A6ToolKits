using A6ToolKits.ApplicationController;
using A6ToolKits.Container;
using A6ToolKits.Controls.Command;
using A6ToolKits.ResourceLoader;
using Avalonia.Media;

namespace A6ToolKits.Layout.Controls.ControlCommand;

/// <summary>
///     关闭 Command
/// </summary>
public sealed class CloseCommand : CommandBase
{
    /// <inheritdoc />
    public override string Text => "关闭程序";

    /// <inheritdoc />
    public override string ToolTip => "关闭程序";

    /// <inheritdoc />
    public override IImage? Image { get; } = ResourceHelper.LoadImage("CloseIcon");

    /// <inheritdoc />
    public override Task Run()
    {
        var window = IoC.GetInstance<IApplicationController>()?.MainWindow;
        window?.Close();
        return Task.CompletedTask;
    }
}
