using A6ToolKits.Common.Attributes.Layout;
using A6ToolKits.Layout.Definer;
using UIDemo.Layout.Actions;

namespace UIDemo.Layout;

public class ToolBar : ToolBarDefiner
{
    [ToolBar(0, "Initials")] public NewFile NewFile { get; } = new();

    [ToolBar(0, "Initials" )] public OpenFile OpenFile { get; } = new();

    [ToolBar(1, "Initials")] public NewFolder NewFolder { get; } = new();

    [ToolBar(1, "Initials")] public OpenFolder OpenFolder { get; } = new();
}