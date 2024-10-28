using System.Threading.Tasks;
using A6ToolKits.Common.Action;
using A6ToolKits.Common.Attributes.Layout;
using Avalonia.Media;

namespace UIDemo.Layout.Actions;

[MenuAction("文件:1")]
[ToolBarAction(1, "Initials")]
public class OpenFile : ActionBase
{
    public override string? Name { get; init; } = "打开文件";
    
    public override IImage? Icon { get; init; } = null;
    
    public override string? ToolTip { get; init; } = "打开文件";

    public override async Task Run()
    {
        await Task.CompletedTask;
    }
}