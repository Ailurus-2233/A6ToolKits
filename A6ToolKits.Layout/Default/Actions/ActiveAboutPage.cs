using A6ToolKits.Action;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace A6ToolKits.Layout.Default.Actions;

public class ActiveAboutPage : ActionBase
{
    public override string? Name { get; init; } = "关于页面";
    public override string? ToolTip { get; init; } = "切换关于页面";

    public override IImage? Icon { get; init; } = null;

    public override Task Run()
    {
        var container = LayoutModule.WindowLayout?.Container;
        container?.ActivatePage("AboutPage");
        return Task.CompletedTask;
    }
}