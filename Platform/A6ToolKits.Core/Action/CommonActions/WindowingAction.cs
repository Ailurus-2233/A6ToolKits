using System.Threading.Tasks;
using A6ToolKits.Bootstrapper;
using A6ToolKits.Helper.Resource;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;

namespace A6ToolKits.Common.Action.CommonActions;

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
        var window = CoreService.Instance.Controller?.GetMainWindow();
        if (window is null) return;
        CanRun = window.WindowState != WindowState.Normal;
        window.PropertyChanged += (sender, args) =>
        {
            if (args.Property.Name == nameof(window.WindowState))
            {
                CanRun = window.WindowState != WindowState.Normal;
            }
        };
    }
}