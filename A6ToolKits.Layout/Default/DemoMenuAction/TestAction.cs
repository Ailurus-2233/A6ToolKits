using A6ToolKits.Layout.ActionMenu;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Serilog;

namespace A6ToolKits.Layout.Default.DemoMenuAction;

public class TestAction : MenuActionDefinition
{
    public override string Text => "Test";
    public override string ToolTip => "Test";
    public override IImage ImageSource { get; } = new DrawingImage();
    public override Task Run()
    {
        Log.Debug("TestAction Run");
        return Task.CompletedTask;
    }
    
    public override IList<MenuActionDefinition> SubCommands { get; } = new List<MenuActionDefinition>();
}