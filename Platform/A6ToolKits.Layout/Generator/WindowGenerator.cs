using A6ToolKits.Helper.Configurator;
using A6ToolKits.Layout.Controls.LayoutWindow;
using A6ToolKits.Layout.Generator.Enums;
using Avalonia.Controls;
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
    ///     生成一个窗口
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    internal Window GenerateWindow(LayoutConfigItem item)
    {
        item.SetToResources();

        Window result = _config.BorderStyle switch
        {
            WindowBorderType.Origin => new OriginWindow(),
            WindowBorderType.Default => new DefaultWindow(),
            WindowBorderType.None => new EmptyWindow(),
            _ => throw new IndexOutOfRangeException("未知的 TopBarType 类型")
        };

        return result;
    }
}