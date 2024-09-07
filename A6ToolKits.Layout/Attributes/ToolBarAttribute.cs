namespace A6ToolKits.Layout.Attributes;

[AttributeUsage(AttributeTargets.Property, Inherited = false)]
public class ToolBarAttribute(int index) : Attribute
{
    public int Index { get; set; } = index;
}