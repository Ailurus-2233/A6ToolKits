// ReSharper disable UnusedType.Global
namespace A6ToolKits.Database.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class TableNameAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}