using A6ToolKits.Common;

namespace A6ToolKits.AssemblyManager.Exceptions;

/// <summary>
///     类型未找到异常，主要用于在自动加载程序集时找不到指定类型时抛出
/// </summary>
/// <param name="typeName">
///     类型名称
/// </param>
/// <param name="details">
///     异常详细信息
/// </param>
internal class TypeNotFoundException(string typeName, string? details)
    : FrameworkExceptionBase(ErrorCode.InvalidArgument, $"Type [{typeName}] not found", details);