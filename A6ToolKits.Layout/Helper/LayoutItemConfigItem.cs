using A6ToolKits.Helper.Config;

namespace A6ToolKits.Layout.Helper;

public class LayoutItemConfigItem : ConfigItemBase
{
    public string Type { get; set; } = string.Empty;
    public string Height { get; set; } = string.Empty;
    public string Width { get; set; } = string.Empty;
    public string Organization { get; set; } = string.Empty;

    public string MainColor { get; set; } = string.Empty;

    public string IconPath { get; set; } = string.Empty;

    public string Position { get; set; } = string.Empty;
    public string Default { get; set; } = string.Empty;
    public string Assembly { get; set; } = string.Empty;
    public string Target { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
}