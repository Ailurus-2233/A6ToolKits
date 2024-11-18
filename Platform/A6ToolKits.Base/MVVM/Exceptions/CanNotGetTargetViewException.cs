using A6ToolKits.Common;

namespace A6ToolKits.MVVM.Exceptions;

public class CanNotGetTargetViewException(Type targetView, string? details = null)
    : FrameworkExceptionBase(ErrorCode.RuntimeError, $"Can not get {targetView} from container", details);