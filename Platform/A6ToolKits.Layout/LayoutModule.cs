using A6ToolKits.Common.Attributes.MVVM;
using A6ToolKits.Layout.Generator;
using A6ToolKits.Module;
using Avalonia.Controls;

namespace A6ToolKits.Layout;

/// <summary>
///     布局模块，如果加载该模块将会基于配置文件自动加载窗口
/// </summary>
[AutoRegister(typeof(LayoutModule), RegisterType.Singleton)]
public class LayoutModule : ModuleBase
{
    /// <summary>
    ///     基于配置文件生成的窗口布局
    /// </summary>
    public Window? Window => WindowGenerator.Window;

    /// <summary>
    ///     初始化布局模块，加载布局配置文件
    /// </summary>
    protected override void Initialize()
    {
        WindowGenerator.Creator = Creator;
        WindowGenerator.GenerateWindow();
    }

    /// <summary>
    ///     重新生成窗口
    /// </summary>
    public void RegenerateWindow()
    {
        WindowGenerator.GenerateWindow();
    }
}