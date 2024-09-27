using System.Threading.Tasks;
using A6ToolKits.Helper.Resource;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;

namespace A6ToolKits.Common.Action.CommonActions;

/// <summary>
///     窗口化动作
/// </summary>
public class WindowAction : ActionBase
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
        var lifetime = Application.Current?.ApplicationLifetime as ClassicDesktopStyleApplicationLifetime;
        var window = lifetime?.MainWindow;
        if (window == null) return Task.CompletedTask;
        window.WindowState = Avalonia.Controls.WindowState.Normal;
        CanRun = window.WindowState != Avalonia.Controls.WindowState.Normal;
        return Task.CompletedTask;
    }
}