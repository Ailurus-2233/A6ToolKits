using A6ToolKits.Module;
using Avalonia;
using Avalonia.Markup.Xaml.Styling;

namespace A6ToolKits.Layout;

public class LayoutModule : ModuleBase
{
    public override string ModuleName { get; set; } = "A6ToolKits.Layout";
    public override string ModuleVersion { get; set; } = "1.0.0";

    public override string ModuleDescription { get; set; } =
        "Add Layout capabilities to A6ToolKits to automatically load layout for main window";

    protected override void Initialize()
    {
        // 自动加载 CoreControls.axaml 到 Avalonia 的资源字典中
        var styleUri = new Uri("avares://A6ToolKits.Layout/LayoutControls.axaml");
        var style = new StyleInclude(styleUri)
        {
            Source = styleUri
        };
        Application.Current?.Styles.Add(style);
        
        // TODO: 自动加载 Menu
        
        // TODO: 自动加载 Page
    }
}