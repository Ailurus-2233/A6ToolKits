using System.Threading.Tasks;
using A6ToolKits.Common.Action;
using A6ToolKits.Common.Attributes.Layout;

namespace UIDemo.Layout.Actions;

[MenuAction("文件:0")]
[ToolBarAction(0, "Initials")]
public class NewFile : ActionBase
{
    public override string? Name { get; init; } = "新建文件";
    
    public override string? ToolTip { get; init; } = "新建文件";
    
    public override async Task Run()
    {
        await Task.CompletedTask;
    }
}