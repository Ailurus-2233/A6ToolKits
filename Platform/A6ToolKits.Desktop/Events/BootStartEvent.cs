namespace A6ToolKits.Events;

/// <summary>
///     启动开始事件
/// </summary>
public class BootStartEvent : EventBase
{
    /// <summary>
    ///     用于日志显示的事件内容
    /// </summary>
    /// <returns>事件内容</returns>
    public override string Message => $"应用启动引导开始，开始时间： {Time:yyyy-MM-dd HH:mm:ss}";
}