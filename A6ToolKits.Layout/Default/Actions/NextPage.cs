using A6ToolKits.Action;
using A6ToolKits.Module;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace A6ToolKits.Layout.Default.Actions;

public class NextPage : ActionBase
{
    public override string? Name { get; init; } = "下一页";
    public override string? ToolTip { get; init; } = "切换到下一页";

    public override IImage? Icon { get; init; } = null;
        // new Bitmap(AssetLoader.Open(new Uri("avares://A6ToolKits.Layout/Assets/next.png")));

    public override Task Run()
    {
        ModuleLoader.TryGetModule<LayoutModule>(out var layoutModule);
        var container = layoutModule?.WindowLayout?.Container;
        container?.MoveToNextPage();
        return Task.CompletedTask;
    }

    public NextPage()
    {
        ModuleLoader.LoadModulesCompleted += (_, _) =>
        {
            ModuleLoader.TryGetModule<LayoutModule>(out var layoutModule);
            var container = layoutModule?.WindowLayout?.Container;
            CanRun = container?.CanMoveToNextPage ?? false;
            if (container != null)
                container.PageChanged += (_, _) =>
                {
                    CanRun = container.CanMoveToNextPage;
                };
        };
    }
}