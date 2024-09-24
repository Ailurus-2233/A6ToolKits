using System.Threading.Tasks;
using A6ToolKits.Action;
using Avalonia.Media;

namespace UIDemo.Default.Actions;

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