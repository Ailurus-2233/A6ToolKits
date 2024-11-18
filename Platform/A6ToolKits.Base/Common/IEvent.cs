namespace A6ToolKits.Common;

/// <summary>
///     事件接口
/// </summary>
public abstract class EventBase
{
    /// <summary>
    ///     用于日志显示的事件内容
    /// </summary>
    public abstract string Message { get; }
}