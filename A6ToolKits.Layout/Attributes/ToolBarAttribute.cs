namespace A6ToolKits.Layout.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ToolBarAttribute(int group, string position)
    : Attribute
{
    public int Group { get; set; } = group;

    public ToolBarPosition Position { get; set; } = Enum.Parse<ToolBarPosition>(position);
}

public enum ToolBarPosition
{
    Left,
    Right
}