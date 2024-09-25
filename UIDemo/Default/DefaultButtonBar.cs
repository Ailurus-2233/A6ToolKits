using A6ToolKits.Attributes;
using A6ToolKits.Layout.Definitions;
using UIDemo.Default.Actions;

namespace UIDemo.Default;

public class DefaultButtonBar : IDefiner
{
    [ToolBar(0, "Left")] public NewFile NewFile { get; } = new();

    [ToolBar(0, "Left")] public OpenFile OpenFile { get; } = new();

    [ToolBar(1, "Left")] public NewFolder NewFolder { get; } = new();

    [ToolBar(1, "Right")] public OpenFolder OpenFolder { get; } = new();

    [ToolBar(2, "Left")] public ActiveAboutPage ActiveAboutPage { get; } = new();
}
