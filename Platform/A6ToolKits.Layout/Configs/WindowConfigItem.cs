using System.Reflection;
using A6ToolKits.Attributes;
using A6Toolkits.Configuration;

namespace A6ToolKits.Layout.Configs;

/// <summary>
///     窗口配置项
/// </summary>
[ConfigName("Window")]
public class WindowConfigItem : ConfigItemBase
{
    /// <summary>
    ///     窗口标题
    /// </summary>
    public string Title { get; set; } = "A6ToolKit-Application";

    /// <summary>
    ///     窗口边框样式，可选值：Default、Origin、None
    /// </summary>
    public string BorderStyle { get; set; } = "Default";
    
    /// <summary>
    ///     窗口类型，可选值：Default、Maximized、FullScreen
    /// </summary>
    public string WindowType { get; set; } = "Default";

    /// <summary>
    ///     窗口宽度
    /// </summary>
    public string Width { get; set; } = "800";

    /// <summary>
    ///     窗口高度
    /// </summary>
    public string Height { get; set; } = "600";

    /// <summary>
    ///     窗口图标
    /// </summary>
    public string Icon { get; set; } = "";

    /// <inheritdoc />
    public override bool IsNecessary => true;

    /// <inheritdoc />
    public override void SetDefault()
    {
        Title = "A6ToolKit-Application";
        BorderStyle = "Default";
        WindowType = "Default";
        Width = "800";
        Height = "600";
        Icon = "";
        Children.Clear();
    }
    
    public void OnLoadedConfig()
    {
        if (string.IsNullOrEmpty(Icon)) return;
        var runAssembly = Assembly.GetEntryAssembly();
        Icon = runAssembly == null ? "" : $"avares://{runAssembly.FullName?.Split(',')[0]}/{Icon}";
    }
}