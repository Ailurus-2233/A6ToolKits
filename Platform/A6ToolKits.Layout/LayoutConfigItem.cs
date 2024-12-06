using A6ToolKits.Configuration;
using A6ToolKits.Configuration.Attributes;
using A6ToolKits.Layout.Configs;


namespace A6ToolKits.Layout;

/// <summary>
///     布局配置项
/// </summary>
[ModuleConfig]
[ConfigName("Layout")]
public class LayoutConfigItem : ConfigItemBase
{
    /// <inheritdoc />
    public override bool IsNecessary => true;

    /// <summary>
    ///     设置默认值
    /// </summary>
    public override void SetDefault()
    {
        Children.Clear();
        Children.Add(new WindowConfigItem());
        Children.Add(new ControlConfigItem());
    }
}