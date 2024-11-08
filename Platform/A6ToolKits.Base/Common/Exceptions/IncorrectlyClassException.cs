using A6ToolKits.Except;

namespace A6ToolKits.Common.Exceptions;

public class IncorrectlyClassException(Type type, Type targetType, string? details = "")
    : FrameworkExceptionBase(ErrorCode.InvalidArgument,
        $"Type [{type.Name}] not match [{targetType.FullName}]", details);