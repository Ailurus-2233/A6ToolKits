using A6ToolKits.Bootstrapper.Interfaces;
using A6ToolKits.Common.Attributes.MVVM;
using A6ToolKits.Layout.Generator;
using A6ToolKits.Module;
using Avalonia.Styling;

namespace A6ToolKits.Layout;

/// <summary>
///     布局模块，如果加载该模块将会基于配置文件自动加载窗口
/// </summary>
[AutoRegister(typeof(LayoutModule), RegisterType.Singleton)]
public sealed class LayoutModule : ModuleBase
{
    private WindowGenerator _generator => WindowGenerator.Instance;

    /// <summary>
    ///     模块名称
    /// </summary>
    protected override string Name => "A6ToolKits.LayoutModule";

    /// <summary>
    ///     初始化布局模块，加载布局配置文件
    /// </summary>
    public override void Initialize()
    {
        var controller = IoC.GetInstance<IApplicationController>();
        if (controller != null)
        {
            controller.SetupMainWindow(_generator.GenerateWindow());
            controller.SetupTheme(ThemeVariant.Light);
        }
        else
        {
            throw new NullReferenceException("BootstrapperService.Instance.ApplicationController is null");
        }
    }
}