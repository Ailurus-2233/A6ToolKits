using A6ToolKits.Helper.Config;
using A6ToolKits.Helper.Instance;
using A6ToolKits.Layout.Controls;
using A6ToolKits.Layout.Controls.Container;
using A6ToolKits.Layout.Controls.LayoutWindow;
using A6ToolKits.Layout.Generator.Enums;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
namespace A6ToolKits.Layout.Generator;

/// <summary>
///     窗口生成器
/// </summary>
internal static class WindowGenerator
{
    /// <summary>
    ///     实例创建器，用于初始化布局过程中控件实例的创建
    /// </summary>
    internal static IInstanceHelper? Creator { get; set; }

    internal static Window Window { get; private set; } = new();

    /// <summary>
    ///     生成一个窗口
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    internal static void GenerateWindow()
    {
        var layoutConfigItem = ConfigHelper.GetElements("Window")?.Item(0);
        if (layoutConfigItem == null)
            throw new Exception("Layout config not found.");
        
        var configItem = new LayoutConfigItem();
        configItem.GenerateFromXmlNode(layoutConfigItem);
        configItem.SetToResources();
        
        Window = WindowConfig.BorderStyle switch
        {
            WindowBorderType.Origin => new OriginWindow(),
            WindowBorderType.Default => new DefaultWindow(),
            WindowBorderType.None => new NoneBorderWindow(),
            _ => throw new IndexOutOfRangeException("未知的 TopBarType 类型")
        };
    }
}