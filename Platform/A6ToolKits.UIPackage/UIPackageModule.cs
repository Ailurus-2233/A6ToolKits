using A6ToolKits.Helper.Instance;
using A6ToolKits.Module;
using Avalonia;
using Avalonia.Markup.Xaml.Styling;

namespace A6ToolKits.UIPackage;

public class UIPackageModule : ModuleBase
{
    protected override void Initialize()
    {
        // var styleUri = new Uri("avares://A6ToolKits.UIPackage/UIPackageStyles.axaml");
        // var style = new StyleInclude(styleUri)
        // {
        //     Source = styleUri
        // };
        // Application.Current?.Styles.Add(style);
        
        var resUri = new Uri("avares://A6ToolKits.UIPackage/UIPackageResources.axaml");
        var resource = new ResourceInclude(resUri)
        {
            Source = resUri
        };

        Application.Current?.Resources.MergedDictionaries.Add(resource);
    }
}