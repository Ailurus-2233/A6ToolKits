using A6ToolKits.Bootstrapper;
using A6ToolKits.Common.Attributes;
using A6ToolKits.Layout.Generator;
using A6ToolKits.Modules;
using Avalonia.Styling;

namespace A6ToolKits.Layout;

/// <summary>
///     布局模块，如果加载该模块将会基于配置文件自动加载窗口
/// </summary>
[AutoRegister(typeof(LayoutModule), RegisterType.Singleton)]
public sealed class LayoutModule : ModuleBase<LayoutConfigItem>
{
    private WindowGenerator _generator => WindowGenerator.Instance;

    /// <summary>
    ///     模块名称
    /// </summary>
    protected override string Name => "A6ToolKits.LayoutModule";

    /// <summary>
    ///     Layout 模块的配置项
    /// </summary>
    protected override LayoutConfigItem Config { get; } = new();

    /// <summary>
    ///     初始化布局模块，加载布局配置文件
    /// </summary>
    public override void Initialize()
    {
        
        var controller = IoC.GetInstance<IWindowController>();
        if (controller != null)
        {
            controller.SetupMainWindow(_generator.GenerateWindow(Config));
            controller.SetupTheme(ThemeVariant.Light);
        }
        else
        {
            throw new NullReferenceException("BootstrapperService.Instance.ApplicationController is null");
        }
    }
}