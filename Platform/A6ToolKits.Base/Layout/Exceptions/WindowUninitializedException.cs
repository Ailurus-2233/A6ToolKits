using A6ToolKits.Common;

namespace A6ToolKits.Layout.Exceptions;

public class WindowUninitializedException(string? details = null)
    : FrameworkExceptionBase(ErrorCode.RuntimeError, "Window is not initialized", details);