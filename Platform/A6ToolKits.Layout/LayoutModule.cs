using A6ToolKits.Common.Attributes.MVVM;
using A6ToolKits.Helper.Instance;
using A6ToolKits.Module;
using Avalonia;
using Avalonia.Controls;
using Serilog;

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
    public Window? Window { get; private set; }
    
    /// <summary>
    ///     初始化布局模块，加载布局配置文件
    /// </summary>
    /// <exception cref="Exception">
    ///     布局加载失败
    /// </exception>
    protected override void Initialize()
    {
        Generator.Creator = Creator;
        Window = Generator.GenerateLayout();
    }
}