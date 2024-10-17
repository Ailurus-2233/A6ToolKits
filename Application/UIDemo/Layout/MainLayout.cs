using A6ToolKits.Layout.Definer;
using Avalonia.Media;

namespace UIDemo.Layout;

public class MainLayout: LayoutDefiner
{
    public override TopBarDefiner TopBarDefiner { get; set; }
    public override StatusBarDefiner StatusBarDefiner { get; set; } = new StatusBar();
    public override PageDefiner PageDefiner { get; set; } = new MainPage();

    public override double StatusBarHeight { get; set; } = 30;
    public override string Title { get; set; } = "UIDemo";
    
    public override Color PrimaryColor { get; set; } = Color.Parse("#FF0000");
    public override double WindowHeight { get; set; } = 600;
    public override double WindowWidth { get; set; } = 800;
    public override TopBarType TopBarType { get; set; }
}