using System.Drawing;
using A6ToolKits.Layout.Definer;
using Avalonia.Styling;

namespace UIDemo.Layout;

public class MainLayout: LayoutDefiner
{
    public override MenuDefiner MenuDefiner { get; set; } = new Menu();
    public override ToolBarDefiner ToolBarDefiner { get; set; } = new ToolBar();
    public override StatusBarDefiner StatusBarDefiner { get; set; } = new StatusBar();
    public override PageDefiner PageDefiner { get; set; } = new MainPage();
    public override double HeaderHeight { get; set; } = 30;
    public override double ToolBarHeight { get; set; } = 30;
    public override double StatusBarHeight { get; set; } = 30;
    public override string Title { get; set; } = "UIDemo";
    
    public override Color PrimaryColor { get; set; }
    public override double WindowHeight { get; set; } = 600;
    public override double WindowWidth { get; set; } = 800;
}