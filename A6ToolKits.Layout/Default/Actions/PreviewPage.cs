using A6ToolKits.Action;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace A6ToolKits.Layout.Default.Actions;

public class PreviewPage: ActionBase
{
    public override string? Name { get; init; } = "上一页";
    public override string? ToolTip { get; init; } = "切换到上一页";

    public override IImage? Icon { get; init; } = null;
        // new Bitmap(AssetLoader.Open(new Uri("avares://A6ToolKits.Layout/Assets/prev.png")));

    public override Task Run()
    {
        LayoutModule.WindowLayout?.Container.MoveToPreviousPage();
        return Task.CompletedTask;
    }
    
    public PreviewPage()
    {
        LayoutModule.LoadLayoutCompleted += (_, _) =>
        {
            var container = LayoutModule.WindowLayout?.Container;
            CanRun = container?.CanMoveToPreviousPage ?? false;
            if (container != null)
                container.PageChanged += (_, _) =>
                {
                    CanRun = container.CanMoveToPreviousPage;
                };
        };
    }
}