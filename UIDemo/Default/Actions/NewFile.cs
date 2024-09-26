using System.Threading.Tasks;
using A6ToolKits.Common.Action;

namespace UIDemo.Default.Actions;

public class NewFile : ActionBase
{
    public override string? Name { get; init; } = "新建文件";
    
    public override string? ToolTip { get; init; } = "新建文件";
    
    public override async Task Run()
    {
        await Task.CompletedTask;
    }
}