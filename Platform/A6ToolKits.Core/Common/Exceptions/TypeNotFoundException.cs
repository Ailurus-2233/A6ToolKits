using System;
namespace A6ToolKits.Common.Exceptions;

/// <summary>
///     用于映射是无法找到目标类所抛出的异常
/// </summary>
public class TypeNotFoundException : Exception
{
    /// <summary>
    ///     构造函数
    /// </summary>
    /// <param name="typeName">
    ///     类的名称
    /// </param>
    public TypeNotFoundException(string typeName) : base($"Type '{typeName}' was not found.")
    {
    }
}