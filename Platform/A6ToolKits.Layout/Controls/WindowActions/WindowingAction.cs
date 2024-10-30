using A6ToolKits.Action;
using A6ToolKits.Event;
using A6ToolKits.Resource;
using Avalonia.Controls;
using Avalonia.Media;

namespace A6ToolKits.Layout.Controls.WindowActions;

/// <summary>
///     窗口化动作
/// </summary>
public sealed class WindowingAction : ActionBase
{
    /// <inheritdoc />
    public override string? Name { get; init; } = "窗口化";

    /// <inheritdoc />
    public override string? ToolTip { get; init; } = "窗口化";

    /// <inheritdoc />
    public override IImage? Icon { get; init; } = ResourceHelper.LoadImage("WindowIcon");

    /// <inheritdoc />
    public override Task Run()
    {
        var window = CoreService.Instance.Controller?.GetMainWindow();
        if (window is null) return Task.CompletedTask;
        window.WindowState = WindowState.Normal;
        return Task.CompletedTask;
    }
    
    /// <summary>
    ///   构造函数
    /// </summary>
    public WindowingAction()
    {
        CoreService.Instance.EventAggregator?.Subscribe<BootFinishedEvent>(e =>
        {
            if (CoreService.Instance.Controller == null) return;
            var window = CoreService.Instance.Controller.GetMainWindow();
            CanRun = window.WindowState != WindowState.Normal;
            window.PropertyChanged += (sender, args) =>
            {
                if (args.Property.Name == nameof(window.WindowState))
                {
                    CanRun = window.WindowState != WindowState.Normal;
                }
            };
        });
    }
}