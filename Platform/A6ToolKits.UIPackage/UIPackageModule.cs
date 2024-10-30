using A6ToolKits.Module;
using Avalonia;
using Avalonia.Markup.Xaml.Styling;

namespace A6ToolKits.UIPackage;

/// <summary>
///     UIPackage 模块
/// </summary>
public sealed class UIPackageModule : ModuleBase
{
    /// <summary>
    ///     模块名称
    /// </summary>
    public override string Name => "A6ToolKits.UIPackage";

    /// <summary>
    ///     构造函数
    /// </summary>
    public override void Initialize()
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