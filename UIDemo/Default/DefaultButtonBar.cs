using A6ToolKits.Attributes;
using A6ToolKits.Layout.Definitions;
using UIDemo.Default.Actions;

namespace UIDemo.Default;

public class DefaultButtonBar : IDefiner
{
    [ButtonBar(0, "Left")] public NewFile NewFile { get; } = new();

    [ButtonBar(0, "Left")] public OpenFile OpenFile { get; } = new();

    [ButtonBar(1, "Left")] public NewFolder NewFolder { get; } = new();

    [ButtonBar(1, "Right")] public OpenFolder OpenFolder { get; } = new();

    [ButtonBar(2, "Left")] public PreviewPage PreviewPage { get; } = new();

    [ButtonBar(2, "Left")] public NextPage NextPage { get; } = new();

    [ButtonBar(2, "Left")] public ActiveAboutPage ActiveAboutPage { get; } = new();
}
