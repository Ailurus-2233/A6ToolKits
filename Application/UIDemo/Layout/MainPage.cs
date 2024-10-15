using A6ToolKits.Layout.Definer;
using Avalonia.Controls;
using UIDemo.Default;

namespace UIDemo.Layout;

public class MainPage : PageDefiner
{
    public override UserControl? LeftPanel { get; set; } = null;
    public override UserControl? RightPanel { get; set; } = null;
    public override UserControl? BottomPanel { get; set; } = null;
    public override UserControl Main { get; set; } = new DefaultPageView();
}