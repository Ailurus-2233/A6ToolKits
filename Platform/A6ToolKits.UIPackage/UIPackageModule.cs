using A6ToolKits.Common.Attributes;
using A6ToolKits.Module;
using A6ToolKits.Modules;
using Avalonia;
using Avalonia.Markup.Xaml.Styling;

namespace A6ToolKits.UIPackage;

/// <summary>
///     UIPackage 模块
/// </summary>
[AutoRegister(typeof(IUIPackageModule), RegisterType.Singleton)]
public sealed class UIPackageModule : ModuleBase, IUIPackageModule
{
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