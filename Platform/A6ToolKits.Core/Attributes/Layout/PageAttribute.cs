using System;

namespace A6ToolKits.Common.Attributes.Layout;

/// <summary>
///     页面属性
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class PageAttribute(string name) : Attribute
{
    /// <summary>
    ///     页面名称
    /// </summary>
    public string Name { get; set; } = name;
}