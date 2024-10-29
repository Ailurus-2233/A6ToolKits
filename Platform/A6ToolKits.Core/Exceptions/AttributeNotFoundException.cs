using System;

namespace A6ToolKits.Exceptions;

/// <summary>
///     属性未找到时所抛出的异常
/// </summary>
public class AttributeNotFoundException: Exception
{
    /// <summary>
    ///     构造函数
    /// </summary>
    /// <param name="type"></param>
    /// <param name="attribute"></param>
    public AttributeNotFoundException(Type type, Type attribute) : base($"Attribute {attribute} not found in type {type.FullName}")
    {
    }
}