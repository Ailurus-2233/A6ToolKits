using A6ToolKits.Common;

namespace A6ToolKits.Layout.Exceptions;

public class AttributeNotFoundException(Type attribute, Type targetType, string? details = "")
    : FrameworkExceptionBase(ErrorCode.InvalidArgument,
        $"Attribute [{attribute.Name}] not found in type [{targetType.FullName}]",
        details);