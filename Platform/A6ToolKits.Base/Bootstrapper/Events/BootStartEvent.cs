using A6ToolKits.Common;

namespace A6ToolKits.Bootstrapper.Events;

/// <summary>
///     启动开始事件
/// </summary>
public class BootStartEvent : EventBase
{
    /// <summary>
    ///     应用启动引导开始时间
    /// </summary>
    public DateTime StartTime { get; set; } = DateTime.Now;

    /// <summary>
    ///     用于日志显示的事件内容
    /// </summary>
    /// <returns>事件内容</returns>
    public override string Message => $"应用启动引导开始，开始时间： {StartTime}";
}