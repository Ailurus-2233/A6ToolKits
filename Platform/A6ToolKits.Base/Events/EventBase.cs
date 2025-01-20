namespace A6ToolKits.Events;

/// <summary>
///     事件基类
/// </summary>
public abstract class EventBase
{
    /// <summary>
    ///     用于日志显示的事件内容
    /// </summary>
    public abstract string Message { get; }
}