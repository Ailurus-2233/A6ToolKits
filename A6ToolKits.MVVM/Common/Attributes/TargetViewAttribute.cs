namespace A6ToolKits.MVVM.Common.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class TargetViewAttribute(Type viewType) : Attribute
{
    public Type ViewType { get; set; } = viewType;
}