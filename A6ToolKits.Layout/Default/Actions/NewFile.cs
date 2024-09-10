using A6ToolKits.Action;

namespace A6ToolKits.Layout.Default.Actions;

/// <inheritdoc />
public class NewFile : ActionBase
{
    /// <inheritdoc />
    public override string? Name { get; init; } = "新建文件";

    /// <inheritdoc />
    public override string? ToolTip { get; init; } = "新建文件";

    /// <inheritdoc />
    public override async Task Run()
    {
        await Task.CompletedTask;
    }
}