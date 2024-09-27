using A6ToolKits.Layout.Definer;

namespace UIDemo.Layout;

public class MainLayout: LayoutDefiner
{
    public override MenuDefiner MenuDefiner { get; set; } = new Menu();
    public override ToolBarDefiner ToolBarDefiner { get; set; } = new ToolBar();
    public override StatusBarDefiner StatusBarDefiner { get; set; } = new StatusBar();
    public override PageDefiner PageDefiner { get; set; } = new MainPage();
    public override double HeaderHeight { get; set; } = 30;
    public override double ToolBarHeight { get; set; } = 25;
    public override double StatusBarHeight { get; set; } = 25;
    public override string Title { get; set; } = "UIDemo";
}