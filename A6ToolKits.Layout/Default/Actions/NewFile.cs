using A6ToolKits.Action;
using A6ToolKits.Layout.Generator;

namespace A6ToolKits.Layout.Default.Actions;

public class NewFile : ActionBase
{
    public override string? Name { get; init; } = "新建文件";
    public override string? ToolTip { get; init; } = "新建文件";

    public override async Task Run()
    {
        await Task.CompletedTask;
    }
}