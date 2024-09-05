using System;
using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.Styling;

namespace A6ToolKits.Module;

public class BaseModule : ModuleBase
{
    public override string ModuleName { get; set; } = "A6ToolKits.Root";
    public override string ModuleVersion { get; set; } = "1.0.0";
    public override string ModuleDescription { get; set; } = "Core of A6ToolKits framework";

    protected override void Initialize()
    {
        // 自动加载 CoreControls.axaml 到 Avalonia 的资源字典中
        var styleUri = new Uri("avares://A6ToolKits/CoreControls.axaml");
        var style = new StyleInclude(styleUri)
        {
            Source = styleUri
        };
        Application.Current?.Styles.Add(style);
    }
}