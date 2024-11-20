using A6ToolKits.Common;

namespace A6ToolKits.MVVM.Exceptions;

/// <summary>
///     不包含 TargetView 属性异常，当 ViewModel 不包含 TargetView 属性时抛出
/// </summary>
/// <param name="type">
///     ViewModel 类型
/// </param>
/// <param name="details">
///     详细信息
/// </param>
public class NotHaveTargetViewProperty(Type type, string? details = null)
    : FrameworkExceptionBase(ErrorCode.InvalidArgument, $"Class: {type} not have TargetView property", details);