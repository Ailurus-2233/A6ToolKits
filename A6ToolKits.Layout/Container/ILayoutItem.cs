using Avalonia.Controls;

namespace A6ToolKits.Layout.Container;

public interface ILayoutItem
{
    public string Name { get; set; }
    public string Target { get; set; }
    public string? Assembly { get; set; }
    public Position Position { get; set; }
    public ContentControl? View { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
}

public enum Position
{
    Left,
    Top,
    Right,
    Bottom,
    Center,
    None
}