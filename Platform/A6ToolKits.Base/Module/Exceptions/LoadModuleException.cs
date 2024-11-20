using A6ToolKits.Common;

namespace A6ToolKits.Module.Exceptions;

/// <summary>
///     加载模块异常
/// </summary>
/// <param name="moduleName">
///     模块名称
/// </param>
/// <param name="details">
///     详细信息
/// </param>
public class LoadModuleException(string moduleName, string? details = "")
    : FrameworkExceptionBase(ErrorCode.RuntimeError, $"Load Module [{moduleName}] failed", details);