namespace A6ToolKits.Configuration.Attributes;

/// <summary>
///     配置项名称
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ConfigNameAttribute(string name) : Attribute
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; } = name;
}