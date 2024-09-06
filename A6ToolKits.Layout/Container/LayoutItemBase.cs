using System.Xml;
using Avalonia.Controls;

namespace A6ToolKits.Layout.Container;

public abstract class LayoutItemBase : ILayoutItem
{
    public required string Name { get; set; }
    public required string Target { get; set; }
    public string? Assembly { get; set; }
    public Position Position { get; set; }
    public ContentControl? View { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }

    public void SetView(ContentControl view)
    {
        View = view;
        View.Width = Width;
        View.Height = Height;
    }

    public void LoadFromXml(XmlNode node)
    {
        Name = node.Attributes?[nameof(Name)]?.Value ?? throw new Exception("Invalid layout configuration");
        Target = node.Attributes?[nameof(Target)]?.Value ?? throw new Exception("Invalid layout configuration");
        Assembly = node.Attributes?[nameof(Assembly)]?.Value;
        Width = double.Parse(node.Attributes?[nameof(Width)]?.Value ?? "0");
        Height = double.Parse(node.Attributes?[nameof(Height)]?.Value ?? "0");
        Position = node.Attributes?[nameof(Position)]?.Value switch
        {
            "Left" => Position.Left,
            "Top" => Position.Top,
            "Right" => Position.Right,
            "Bottom" => Position.Bottom,
            "Center" => Position.Center,
            _ => Position.None
        };
    }
}