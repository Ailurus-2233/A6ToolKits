using System;

namespace A6ToolKits.Common.Attributes.Layout;

/// <summary>
///     定义者的表示，表示这个类是一个Layout的定义者
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class LayoutDefinerAttribute(string name) : Attribute
{
    /// <summary>
    ///     Layout的名称
    /// </summary>
    public string Name { get; set; } = name;
}