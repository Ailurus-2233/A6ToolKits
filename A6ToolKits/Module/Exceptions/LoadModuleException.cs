namespace A6ToolKits.Module.Exceptions;

/// <summary>
/// 模块加载异常
/// </summary>
public class LoadModuleException(string message) : System.Exception(message);