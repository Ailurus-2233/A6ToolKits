namespace A6ToolKits.Exceptions;

/// <summary>
///     无法获取目标视图异常，当无法从容器中获取目标视图时抛出
/// </summary>
/// <param name="targetView">
///     目标视图
/// </param>
/// <param name="details">
///     详细信息
/// </param>
public class CanNotGetTargetViewException(Type targetView, string? details = null)
    : ExceptionBase(ErrorCode.RuntimeError, $"Can not get {targetView} from container", details);