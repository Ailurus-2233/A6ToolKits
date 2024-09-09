using A6ToolKits.Layout.Attributes;
using A6ToolKits.Layout.Default.Actions;
using A6ToolKits.Layout.Definitions;

namespace A6ToolKits.Layout.Default;

public class DefaultToolBar : IDefinition
{
    [ToolBar(0, "Left")] public NewFile NewFile { get; } = new();

    [ToolBar(0, "Left")] public OpenFile OpenFile { get; } = new();

    [ToolBar(1, "Left")] public NewFolder NewFolder { get; } = new();

    [ToolBar(1, "Right")] public OpenFolder OpenFolder { get; } = new();
}