using A6ToolKits.Common;

namespace A6ToolKits.Layout.Exceptions;

/// <summary>
///     类型不匹配异常，主要目标类型和实际类型不匹配时抛出
/// </summary>
/// <param name="type">
///     实际类型
/// </param>
/// <param name="targetType">
///     目标类型
/// </param>
/// <param name="details">
///     异常详细信息
/// </param>
public class IncorrectlyClassException(Type type, Type targetType, string? details = "")
    : FrameworkExceptionBase(ErrorCode.InvalidArgument,
        $"Type [{type.Name}] not match [{targetType.FullName}]", details);