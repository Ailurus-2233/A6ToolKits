using A6ToolKits.Layout.ActionMenu;
using Avalonia.Media;

namespace A6ToolKits.Layout.Default.DemoMenuAction;

public class FileGroup : MenuActionDefinition
{
    public override string Text => "文件";
    public override string ToolTip => "文件";
    public override IImage ImageSource { get; } = new DrawingImage();
    public override Task Run()
    {
        return Task.CompletedTask;
    }

    public override IList<MenuActionDefinition> SubCommands { get; } = [new AboutGroup(), new TestAction()];
}