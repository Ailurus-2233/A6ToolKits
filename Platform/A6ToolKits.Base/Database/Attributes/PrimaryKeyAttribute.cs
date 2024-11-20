// ReSharper disable ClassNeverInstantiated.Global
namespace A6ToolKits.Database.Attributes;

/// <summary>
///     主键特性, 用于标记数据库中的主键
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class PrimaryKeyAttribute: Attribute;