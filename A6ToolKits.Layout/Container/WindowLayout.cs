using System.ComponentModel;
using A6ToolKits.Layout.Container.Controls;
using Avalonia.Controls;
using Avalonia.Media;

namespace A6ToolKits.Layout.Container;

public class WindowLayout
{
    public WindowContainer WindowContainer { get; } = new();

    public Menu Menu => WindowContainer.Menu;

    public StackPanel LeftPanel => WindowContainer.LeftDock;

    public StackPanel RightPanel => WindowContainer.RightDock;

    public StackPanel TopPanel => WindowContainer.TopDock;

    public StackPanel BottomPanel => WindowContainer.BottomDock;

    public DockPanel StatusBar => WindowContainer.StatusBar;

    public WindowType Type { get; set; } = WindowType.Window;
    public double Width { get; set; } = 800;
    public double Height { get; set; } = 600;
    
    public Color MainColor { get; set; } = Colors.CornflowerBlue;

    public PageContainer Container => WindowContainer.Page;
}

public enum WindowType
{
    [Description("Window")] Window,
    [Description("FullScreen")] FullScreen
}