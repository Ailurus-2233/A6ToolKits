using System;

namespace A6ToolKits.Common.Attributes.MVVM;

/// <summary>
///     一个属性，用于标记需要注入的属性
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class InjectAttribute : Attribute
{
}