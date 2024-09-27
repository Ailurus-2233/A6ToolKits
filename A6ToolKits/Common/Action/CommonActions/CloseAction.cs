using System.Threading.Tasks;
using A6ToolKits.Helper.Resource;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;

namespace A6ToolKits.Common.Action.CommonActions;

/// <summary>
///     关闭程序动作
/// </summary>
public class CloseAction : ActionBase
{
    /// <inheritdoc />
    public override string? Name { get; init; } = "关闭程序";

    /// <inheritdoc />
    public override string? ToolTip { get; init; } = "关闭程序";

    /// <inheritdoc />
    public override IImage? Icon { get; init; } = ResourceHelper.LoadImage("CloseIcon");

    /// <inheritdoc />
    public override Task Run()
    {
        var lifetime = Application.Current?.ApplicationLifetime as ClassicDesktopStyleApplicationLifetime;
        lifetime?.Shutdown();
        return Task.CompletedTask;
    }
}