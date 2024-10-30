using A6ToolKits.Config;
using A6ToolKits.Instance;
using A6ToolKits.Layout.Controls;
using A6ToolKits.Layout.Controls.Container;
using A6ToolKits.Layout.Controls.LayoutWindow;
using A6ToolKits.Layout.Generator.Enums;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using DefaultWindow = A6ToolKits.Layout.Controls.LayoutWindow.DefaultWindow;

namespace A6ToolKits.Layout.Generator;

/// <summary>
///     窗口生成器
/// </summary>
internal class WindowGenerator
{
    private static readonly Lazy<WindowGenerator> lazy = new(() => new WindowGenerator());

    private WindowGenerator()
    {
        
    }
    
    /// <summary>
    ///     窗口生成器实例
    /// </summary>
    public static WindowGenerator Instance => lazy.Value;
    
    private static WindowConfig _config => WindowConfig.Instance;

    /// <summary>
    ///     实例创建器，用于初始化布局过程中控件实例的创建
    /// </summary>
    public IInstanceCreator? Creator { get; set; } = CoreService.Instance.Creator;
    
    /// <summary>
    ///     生成一个窗口
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    internal Window GenerateWindow()
    {
        var layoutConfigItem = ConfigHelper.GetElements("Window")?.Item(0);
        if (layoutConfigItem == null)
            throw new Exception("Layout config not found.");
        
        var configItem = new LayoutConfigItem();
        configItem.GenerateFromXmlNode(layoutConfigItem);
        configItem.SetToResources();
        
        Window result = _config.BorderStyle switch
        {
            WindowBorderType.Origin => new OriginWindow(),
            WindowBorderType.Default => new DefaultWindow(),
            WindowBorderType.None => new NoneBorderWindow(),
            _ => throw new IndexOutOfRangeException("未知的 TopBarType 类型")
        };

        return result;
    }
}