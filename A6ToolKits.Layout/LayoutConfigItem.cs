using A6ToolKits.Helper.Config;

namespace A6ToolKits.Layout;

public class LayoutConfigItem : ConfigItemBase
{
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Height { get; set; }
    public string? Width { get; set; }
    public string? BackgroundColor { get; set; }
    public string? PrimaryColor { get; set; }
    public string? Assembly { get; set; }
    public string? Target { get; set; }
    public string? Icon { get; set; }
}