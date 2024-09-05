namespace A6ToolKits.Layout.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class PageAttribute(string name) : Attribute
{
    public string Name { get; set; } = name;
}