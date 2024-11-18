using A6ToolKits.Common;

namespace A6ToolKits.MVVM.Exceptions;

public class NotHaveTargetViewProperty(Type type, string? details = null)
    : FrameworkExceptionBase(ErrorCode.InvalidArgument, $"Class: {type} not have TargetView property", details);