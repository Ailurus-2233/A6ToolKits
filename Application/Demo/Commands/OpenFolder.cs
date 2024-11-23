using System;
using System.Threading.Tasks;
using A6ToolKits.Command;
using A6ToolKits.Common.ResourceLoader;
using A6ToolKits.Layout.Attributes;
using A6ToolKits.ResourceLoader;
using Avalonia.Media;

namespace UIDemo.Commands;

[MenuAction(MenuItemType.IconAndText, "打开:1")]
[ToolBarAction(1, ButtonType.Icon)]
public class OpenFolder : CommandBase
{
    public override string Name => "打开文件夹";
    public override string ToolTip => "打开文件夹";
    public override IImage Image => AssetHelper.LoadSvgImage(new Uri("avares://Demo/Assets/Icons/folder-edit.svg"));

    public override Task Run()
    {
        return Task.CompletedTask;
    }
}