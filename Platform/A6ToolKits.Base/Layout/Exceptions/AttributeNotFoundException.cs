using A6ToolKits.Common;

namespace A6ToolKits.Layout.Exceptions;

/// <summary>
///     属性未找到异常，主要用于在自动加载程序集时找不到指定属性时抛出
/// </summary>
/// <param name="attribute">
///     属性类型
/// </param>
/// <param name="targetType">
///     目标类型
/// </param>
/// <param name="details">
///     异常详细信息
/// </param>
public class AttributeNotFoundException(Type attribute, Type targetType, string? details = "")
    : FrameworkExceptionBase(ErrorCode.InvalidArgument,
        $"Attribute [{attribute.Name}] not found in type [{targetType.FullName}]",
        details);