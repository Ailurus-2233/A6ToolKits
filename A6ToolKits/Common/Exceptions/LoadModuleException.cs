using System;
using System.Reactive.PlatformServices;

namespace A6ToolKits.Common.Exceptions;

/// <summary>
/// 模块加载异常
/// </summary>
public class LoadModuleException(string message) : Exception(message);