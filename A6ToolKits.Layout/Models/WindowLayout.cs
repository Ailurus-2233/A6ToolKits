using System.ComponentModel;

namespace A6ToolKits.Layout.Models;

public class WindowLayout
{
    public Controls.WindowContainer WindowContainer { get; set; } = new();
    public bool IsContainMenu { get; set; }
    public bool IsContainToolBar { get; set; }
    public bool IsContainStatusBar { get; set; }
    public bool IsContainTabBar { get; set; }

    public WindowType WindowType { get; set; } = WindowType.Window;
    public double Width { get; set; } = 800;
    public double Height { get; set; } = 600;
}

public enum WindowType
{
    [Description("Window")] Window,
    [Description("FullScreen")] FullScreen,
}