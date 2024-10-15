using System.Threading.Tasks;
using A6ToolKits.Helper.Resource;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;

namespace A6ToolKits.Common.Action.CommonActions;

/// <summary>
///     最小化动作
/// </summary>
public class MinusAction : ActionBase
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
        var lifetime = Application.Current?.ApplicationLifetime as ClassicDesktopStyleApplicationLifetime;
        var window = lifetime?.MainWindow;
        if (window == null) return Task.CompletedTask;
        window.WindowState = Avalonia.Controls.WindowState.Minimized;
        return Task.CompletedTask;
    }
}