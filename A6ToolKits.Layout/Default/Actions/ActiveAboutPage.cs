using A6ToolKits.Action;
using A6ToolKits.Module;
using Avalonia.Media;

namespace A6ToolKits.Layout.Default.Actions;

/// <inheritdoc />
public class ActiveAboutPage : ActionBase
{
    /// <inheritdoc />
    public override string? Name { get; init; } = "关于页面";

    /// <inheritdoc />
    public override string? ToolTip { get; init; } = "切换关于页面";

    /// <inheritdoc />
    public override IImage? Icon { get; init; } = null;

    /// <inheritdoc />
    public override Task Run()
    {
        ModuleLoader.TryGetModule<LayoutModule>(out var layoutModule);
        var container = layoutModule?.WindowLayout?.Container;
        container?.ActivatePage("AboutPage");
        return Task.CompletedTask;
    }
}