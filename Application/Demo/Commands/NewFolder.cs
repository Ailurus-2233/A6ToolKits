using System;
using System.Threading.Tasks;
using A6ToolKits.Base.Commands;
using A6ToolKits.Layout.Attributes;
using A6ToolKits.ResourceLoader;
using Avalonia.Media;

namespace UIDemo.Commands;

[MenuAction(MenuItemType.IconAndText, "新建:0")]
[ToolBarAction(0, ButtonType.Icon)]
public class NewFolder : CommandBase
{
    public override string Name => "新建文件夹";
    public override string ToolTip => "新建文件夹";
    public override IImage Image => AssetHelper.LoadSvgImage(new Uri("avares://Demo/Assets/Icons/folder-new.svg"));
    public override Task Run()
    {
        return Task.CompletedTask;
    }
}