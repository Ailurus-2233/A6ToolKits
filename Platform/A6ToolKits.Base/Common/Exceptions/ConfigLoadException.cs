using A6ToolKits.Except;

namespace A6ToolKits.Common.Exceptions;

public class ConfigLoadException(Type type, string? details = "")
    : FrameworkExceptionBase(ErrorCode.RuntimeError, $"Load Config [{type.FullName}] failed", details);