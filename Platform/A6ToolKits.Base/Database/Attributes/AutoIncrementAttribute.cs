namespace A6ToolKits.Database.Attributes;

/// <summary>
///     数据库自增长属性
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class AutoIncrementAttribute: Attribute;