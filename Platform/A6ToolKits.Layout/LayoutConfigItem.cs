using System.Reflection;
using System.Xml;
using A6ToolKits.Configuration;
using A6ToolKits.Configuration.Attributes;
using A6ToolKits.Configuration.Exceptions;
using A6ToolKits.Layout.Generator;
using A6ToolKits.Layout.Generator.Enums;
using Avalonia.Media;
using Avalonia.Styling;

namespace A6ToolKits.Layout;

/// <summary>
///     布局配置项
/// </summary>
[ModuleConfig]
[ConfigName("Window")]
public class LayoutConfigItem : ConfigItemBase
{
    /// <summary>
    ///     窗口标题
    /// </summary>
    public string Title { get; set; } = "A6ToolKit-Application";

    /// <summary>
    ///     窗口边框样式
    /// </summary>
    public string BorderStyle { get; set; } = "Default";

    /// <summary>
    ///     窗口宽度
    /// </summary>
    public string Width { get; set; } = "800";

    /// <summary>
    ///     窗口高度
    /// </summary>
    public string Height { get; set; } = "600";

    /// <summary>
    ///     窗口重点色
    /// </summary>
    public string PrimaryColor { get; set; } = "#6495ED";

    /// <summary>
    ///     窗口背景色
    /// </summary>
    public string BackgroundColor { get; set; } = "#FFFFFF";

    /// <summary>
    ///     窗口图标
    /// </summary>
    public string Icon { get; set; } = "";
    

    /// <inheritdoc />
    public override bool IsNecessary => true;

    /// <summary>
    ///     从配置文件中加载配置
    /// </summary>
    /// <exception cref="Exception">
    ///     配置文件未找到
    /// </exception>
    public override void OnLoadedConfig()
    {
        if (string.IsNullOrEmpty(Icon)) return;
        var runAssembly = Assembly.GetEntryAssembly();
        Icon = runAssembly == null ? "" : $"avares://{runAssembly.FullName?.Split(',')[0]}/{Icon}";
    }

    /// <summary>
    ///     设置默认值
    /// </summary>
    public override void SetDefault()
    {
        Title = "A6ToolKit-Application";
        BorderStyle = "Default";
        Width = "800";
        Height = "600";
        PrimaryColor = "#6495ED";
        BackgroundColor = "#FFFFFF";
        Icon = "";
        Children.Clear();
    }
}