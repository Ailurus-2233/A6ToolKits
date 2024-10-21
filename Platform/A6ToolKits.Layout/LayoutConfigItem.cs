using A6ToolKits.Helper.Config;
using A6ToolKits.Layout.ControlGenerator;
using A6ToolKits.Layout.ControlGenerator.Enums;
using A6ToolKits.Layout.Generator;
using Avalonia.Media;

namespace A6ToolKits.Layout;

/// <summary>
///     布局配置项
/// </summary>
public class LayoutConfigItem : ConfigItemBase
{
    /// <summary>
    ///     窗口标题
    /// </summary>
    public string Title { get; set; } = "";

    /// <summary>
    ///     窗口边框样式
    /// </summary>
    public string BorderStyle { get; set; } = "";

    /// <summary>
    ///     窗口宽度
    /// </summary>
    public string Width { get; set; } = "";

    /// <summary>
    ///     窗口高度
    /// </summary>
    public string Height { get; set; } = "";

    /// <summary>
    ///     窗口重点色
    /// </summary>
    public string PrimaryColor { get; set; } = "";

    internal void SetToResources()
    {
        if (!string.IsNullOrWhiteSpace(Title))
        {
            WindowConfig.Title = Title;
        }

        if (!string.IsNullOrWhiteSpace(Width))
        {
            WindowConfig.Width = double.Parse(Width);
        }

        if (!string.IsNullOrWhiteSpace(Height))
        {
            WindowConfig.Height = double.Parse(Height);
        }

        if (!string.IsNullOrWhiteSpace(PrimaryColor))
        {
            WindowConfig.PrimaryColor = Color.Parse(PrimaryColor);
        }

        if (!string.IsNullOrWhiteSpace(BorderStyle))
        {
            try
            {
                WindowConfig.BorderStyle = Enum.Parse<WindowBorderType>(BorderStyle);
            }
            catch
            {
                WindowConfig.BorderStyle = WindowBorderType.Default;
            }
        }
    }
}