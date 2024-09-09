using A6ToolKits.Action;
using A6ToolKits.Layout.Generator;
using Avalonia.Media;

namespace A6ToolKits.Layout.Default.Actions;

public class NewFolder : ActionBase
{
    public override string? Name { get; init; } = "新建文件夹";
    public override IImage? Icon { get; init; } = null;
    
    public override string? ToolTip { get; init; } = "新建文件夹";

    public override async Task Run()
    {
        await Task.CompletedTask;
    }
}