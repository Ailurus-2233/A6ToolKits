using A6ToolKits.Common;

namespace A6ToolKits.Layout.Exceptions;

/// <summary>
///     窗口未初始化异常
/// </summary>
/// <param name="details">
///     详细信息
/// </param>
public class WindowUninitializedException(string? details = null)
    : FrameworkExceptionBase(ErrorCode.RuntimeError, "Window is not initialized", details);