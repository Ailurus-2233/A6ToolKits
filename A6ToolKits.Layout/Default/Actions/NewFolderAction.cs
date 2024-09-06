using A6ToolKits.Action;
using Avalonia.Media;

namespace A6ToolKits.Layout.Default.Actions;

public class NewFolderAction: ActionBase
{
    public override string Name { get; init; } = "新建文件夹";
    public override IImage? Icon { get; init; } = null;
    
    public override async Task Run()
    {
        await Task.CompletedTask;
    }
}