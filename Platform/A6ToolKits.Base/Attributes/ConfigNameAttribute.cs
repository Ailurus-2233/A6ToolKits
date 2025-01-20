namespace A6ToolKits.Attributes;

/// <summary>
///     配置项名称
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ConfigNameAttribute(string name) : AttributeBase
{
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; } = name;
}