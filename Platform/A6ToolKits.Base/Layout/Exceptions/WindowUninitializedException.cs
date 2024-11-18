namespace A6ToolKits.Common.Exceptions;

public class WindowUninitializedException(string information, string? details = null)
    : FrameworkExceptionBase(ErrorCode.RuntimeError, information, details);