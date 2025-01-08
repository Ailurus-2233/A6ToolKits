// ReSharper disable UnusedType.Global
namespace A6ToolKits.Database.Attributes;

/// <summary>
///     表名特性，用于标记数据库中的表名
/// </summary>
/// <param name="name">
///     表名
/// </param>
[AttributeUsage(AttributeTargets.Class)]
public class TableNameAttribute(string name) : Attribute
{
    /// <summary>
    ///     表名
    /// </summary>
    public string Name { get; } = name;
}