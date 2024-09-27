using System.Threading.Tasks;
using A6ToolKits.Common.Action;
using Avalonia.Media;

namespace UIDemo.Layout.Actions;

public class OpenFolder : ActionBase
{
    public override string? Name { get; init; } = "打开文件夹";
    
    public override IImage? Icon { get; init; } = null;
    
    public override string? ToolTip { get; init; } = "打开文件夹";
    
    public override async Task Run()
    {
        await Task.CompletedTask;
    }
}