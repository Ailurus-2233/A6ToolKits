using System.Reflection;
using A6ToolKits.Configuration;
using A6ToolKits.Configuration.Attributes;

namespace A6ToolKits.Layout.Configs;

/// <summary>
///     主页配置项
/// </summary>
[ConfigName("MainPage")]
public class MainPageConfigItem : ConfigItemBase
{
    /// <summary>
    ///     目标类型
    /// </summary>
    public string TargetType { get; set; } = string.Empty;
    
    /// <inheritdoc />
    public override bool IsNecessary => false;

    /// <inheritdoc />
    public override void SetDefault()
    {
        TargetType = "MainPage";
        Children.Clear();
    }

    /// <summary>
    ///     获取目标页面类型
    /// </summary>
    /// <returns>
    ///     目标页面类型
    /// </returns>
    public Type? FindTargetType()
    {
        var assembly = Assembly.GetEntryAssembly();
        return assembly?.GetType(TargetType);
    }
}