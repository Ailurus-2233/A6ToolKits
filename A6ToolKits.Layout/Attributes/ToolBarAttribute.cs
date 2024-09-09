namespace A6ToolKits.Layout.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ToolBarAttribute(int index, int group, string type, string position)
    : Attribute
{
    public int Index { get; set; } = index;
    public int Group { get; set; } = group;

    public ButtonType Type { get; set; } = Enum.Parse<ButtonType>(type);

    public Position Position { get; set; } = Enum.Parse<Position>(position);
}

public enum ButtonType
{
    Icon,
    Text,
    IconAndText,
    Initials
}

public enum Position
{
    Left,
    Right
}