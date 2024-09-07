using A6ToolKits.Action;
using A6ToolKits.Layout.Attributes;
using A6ToolKits.Layout.Generate;
using Avalonia.Media;

namespace A6ToolKits.Layout.Default.Actions;

public class OpenFile: MenuActionBase
{
    public override string? Name { get; init; } = "打开文件";
    public override IImage? Icon { get; init; } = null;
    
    public override async Task Run()
    {
        await Task.CompletedTask;
    }
}