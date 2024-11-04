using System;
using System.Threading.Tasks;
using A6ToolKits.Attributes.Layout;
using A6ToolKits.Commands;
using A6ToolKits.Common.Attributes.Layout;
using A6ToolKits.Resource;
using Avalonia.Media;

namespace UIDemo.Layout.Commands;

[MenuAction(MenuItemType.IconAndText, "打开:1")]
[ToolBarAction(1, ButtonType.Icon)]
public class OpenFolder : CommandBase
{
    public override string? Name => "打开文件夹";
    public override string? ToolTip => "打开文件夹";
    public override IImage Image => AssetHelper.LoadSvgImage(new Uri("avares://UIDemo/Resources/Icons/folder-edit.svg"));

    public override Task Run()
    {
        return Task.CompletedTask;
    }
}