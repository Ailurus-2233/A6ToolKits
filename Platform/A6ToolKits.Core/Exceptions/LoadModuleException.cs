using System;

namespace A6ToolKits.Exceptions;

/// <summary>
///     模块加载异常
/// </summary>
public class LoadModuleException(string message) : Exception(message);