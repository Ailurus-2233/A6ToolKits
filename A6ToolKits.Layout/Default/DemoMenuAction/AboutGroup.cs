using A6ToolKits.Layout.ActionMenu;
using Avalonia.Media;

namespace A6ToolKits.Layout.Default.DemoMenuAction;

public class AboutGroup : MenuActionDefinition
{
    public override string Text => "关于";
    public override string ToolTip => "关于";
    public override IImage ImageSource { get; } = new DrawingImage();

    public override Task Run()
    {
        return Task.CompletedTask;
    }

    public override IList<MenuActionDefinition> SubCommands { get; } = [new TestAction(), new TestAction()];
}
    