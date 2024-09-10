using A6ToolKits.Action;
using A6ToolKits.Module;
using Avalonia.Media;

namespace A6ToolKits.Layout.Default.Actions;

/// <inheritdoc />
public class PreviewPage : ActionBase
{
    /// <inheritdoc />
    public PreviewPage()
    {
        ModuleLoader.LoadModulesCompleted += (_, _) =>
        {
            ModuleLoader.TryGetModule<LayoutModule>(out var layoutModule);
            var container = layoutModule?.WindowLayout?.Container;
            CanRun = container?.CanMoveToPreviousPage ?? false;
            if (container != null)
                container.PageChanged += (_, _) => { CanRun = container.CanMoveToPreviousPage; };
        };
    }

    /// <inheritdoc />
    public override string? Name { get; init; } = "上一页";

    /// <inheritdoc />
    public override string? ToolTip { get; init; } = "切换到上一页";

    /// <inheritdoc />
    public override IImage? Icon { get; init; } = null;
    // new Bitmap(AssetLoader.Open(new Uri("avares://A6ToolKits.Layout/Assets/prev.png")));

    /// <inheritdoc />
    public override Task Run()
    {
        ModuleLoader.TryGetModule<LayoutModule>(out var layoutModule);
        layoutModule?.WindowLayout?.Container.MoveToPreviousPage();
        return Task.CompletedTask;
    }
}