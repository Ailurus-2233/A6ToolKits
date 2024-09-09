namespace A6ToolKits.Layout.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class StatusBarAttribute(string position) : Attribute
{
    public StatusPosition Position { get; set; } = Enum.Parse<StatusPosition>(position);
}

public enum StatusPosition
{
    Left,
    Center,
    Right
}