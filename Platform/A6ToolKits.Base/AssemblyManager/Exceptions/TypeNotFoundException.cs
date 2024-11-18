using A6ToolKits.Common;

namespace A6ToolKits.AssemblyManager.Exceptions;

public class TypeNotFoundException(string typeName, string? details)
    : FrameworkExceptionBase(ErrorCode.InvalidArgument, $"Type [{typeName}] not found", details);