namespace A6ToolKits.Common.Exceptions;

public class LoadModuleException(string moduleName, string? details = "")
    : FrameworkExceptionBase(ErrorCode.RuntimeError, $"Load Module [{moduleName}] failed", details);