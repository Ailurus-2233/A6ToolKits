using System.Threading.Tasks;
using A6ToolKits.Bootstrapper;
using A6ToolKits.Helper.Resource;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;

namespace A6ToolKits.Common.Action.CommonActions;

/// <summary>
///     最大化动作
/// </summary>
public sealed class MaximizeAction : ActionBase
{
    private static Window? _window => CoreService.Instance.Controller?.GetMainWindow();
    
    /// <inheritdoc />
    public override string? Name { get; init; } = "最大化";

    /// <inheritdoc />
    public override string? ToolTip { get; init; } = "最大化";

    /// <inheritdoc />
    public override IImage? Icon { get; init; } = ResourceHelper.LoadImage("MaximizeIcon");

    /// <inheritdoc />
    public override Task Run()
    {
        if (_window is null) return Task.CompletedTask;
        _window.WindowState = WindowState.Maximized;
        return Task.CompletedTask;
    }

    /// <summary>
    ///    构造函数
    /// </summary>
    public MaximizeAction()
    {
        if (_window == null) return;
        CanRun = _window.WindowState != WindowState.Maximized;
        _window.PropertyChanged += (sender, args) =>
        {
            if (args.Property.Name == nameof(_window.WindowState))
            {
                CanRun = _window.WindowState != WindowState.Maximized;
            }
        };
    }
}