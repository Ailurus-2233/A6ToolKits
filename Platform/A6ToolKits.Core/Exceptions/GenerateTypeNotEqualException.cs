using System;

namespace A6ToolKits.Exceptions;

/// <summary>
///     生成时的异常
/// </summary>
public class GenerateTypeNotEqualException : Exception
{
    /// <summary>
    ///     生成类型不匹配
    /// </summary>
    /// <param name="currentType">
    ///     当前类型
    /// </param>
    /// <param name="targetType">
    ///     目标类型
    /// </param>
    public GenerateTypeNotEqualException(string currentType, string targetType) : base(
        $"生成类型不匹配，当前类型：{currentType}，目标类型：{targetType}")
    {
    }
}