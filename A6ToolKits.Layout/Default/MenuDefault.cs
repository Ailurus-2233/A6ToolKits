using A6ToolKits.Action;
using A6ToolKits.Layout.Container;
using A6ToolKits.Layout.Default.Actions;

namespace A6ToolKits.Layout.Default;

public class MenuDefault : LayoutMenu
{
    public override List<LayoutMenuItem> MenuItems { get; set; } =
    [
        new LayoutMenuItem
        {
            GroupName = "新建",
            IsGroup = true,
            SubItemGroups =
            [
                [
                    new LayoutMenuItem() { Action = new NewFileAction()},
                    new LayoutMenuItem() { Action = new NewFolderAction()}
                ],
                [
                    new LayoutMenuItem() { Action = new NewFileAction()},
                    new LayoutMenuItem() { Action = new NewFolderAction()}
                ]
            ]
        },
        new LayoutMenuItem
        {
            GroupName = "打开",
            IsGroup = true,
            SubItemGroups =
            [
                [
                    new LayoutMenuItem() { Action = new OpenFileAction()},
                    new LayoutMenuItem() { Action = new OpenFolderAction()}
                ]
            ]
        },
        new LayoutMenuItem
        {
            GroupName = "保存",
            IsGroup = true,
            IsEnabled = false
        }
    ];
}