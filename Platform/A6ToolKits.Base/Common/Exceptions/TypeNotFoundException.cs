using A6ToolKits.Except;

namespace A6ToolKits.Common.Exceptions;

public class TypeNotFoundException(string typeName, string? details)
    : FrameworkExceptionBase(ErrorCode.InvalidArgument, $"Type [{typeName}] not found", details);