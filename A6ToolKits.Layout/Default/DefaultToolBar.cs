using A6ToolKits.Layout.Attributes;
using A6ToolKits.Layout.Default.Actions;
using A6ToolKits.Layout.Definitions;

namespace A6ToolKits.Layout.Default;

public class DefaultToolBar : IDefinition
{
    [ToolBar(0, 0, "Text", "Left")] public NewFile NewFile { get; } = new();

    [ToolBar(1, 0, "Text", "Left")] public OpenFile OpenFile { get; } = new();

    [ToolBar(0, 1, "Text", "Left")] public NewFolder NewFolder { get; } = new();

    [ToolBar(1, 1, "Text", "Right")] public OpenFolder OpenFolder { get; } = new();
}