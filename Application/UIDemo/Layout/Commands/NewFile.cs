using System;
using System.Threading.Tasks;
using A6ToolKits.Attributes.Layout;
using A6ToolKits.Commands;
using A6ToolKits.Common.Attributes.Layout;
using A6ToolKits.Resource;
using Avalonia.Media;

namespace UIDemo.Layout.Commands;

[MenuAction(MenuItemType.IconAndText, "新建:0")]
[ToolBarAction(0, ButtonType.Icon)]
public class NewFile : CommandBase
{
    public override string? Name => "新建文件";

    public override string? ToolTip => "新建文件";
    public override IImage Image => AssetHelper.LoadSvgImage(new Uri("avares://UIDemo/Resources/Icons/file-add.svg"));

    public override Task Run()
    {   
        return Task.CompletedTask;
    }
}