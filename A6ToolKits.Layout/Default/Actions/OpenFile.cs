using A6ToolKits.Action;
using Avalonia.Media;

namespace A6ToolKits.Layout.Default.Actions;

/// <inheritdoc />
public class OpenFile : ActionBase
{
    /// <inheritdoc />
    public override string? Name { get; init; } = "打开文件";

    /// <inheritdoc />
    public override IImage? Icon { get; init; } = null;

    /// <inheritdoc />
    public override string? ToolTip { get; init; } = "打开文件";

    /// <inheritdoc />
    public override async Task Run()
    {
        await Task.CompletedTask;
    }
}