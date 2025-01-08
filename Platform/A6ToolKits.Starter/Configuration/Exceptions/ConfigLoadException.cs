using A6ToolKits.Common;

namespace A6ToolKits.Configuration.Exceptions;

/// <summary>
///     配置加载异常，主要用于配置加载失败时抛出
/// </summary>
/// <param name="type">
///     配置项类
/// </param>
/// <param name="details">
///     异常详细信息
/// </param>
public class ConfigLoadException(Type type, string? details = "")
    : FrameworkExceptionBase(ErrorCode.RuntimeError, $"Load Config [{type.FullName}] failed", details);