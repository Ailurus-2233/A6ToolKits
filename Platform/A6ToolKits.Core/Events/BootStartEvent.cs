using System;

namespace A6ToolKits.Events;

/// <summary>
///     启动开始事件
/// </summary>
public class BootStartEvent : IEvent
{
    /// <summary>
    ///     应用启动引导开始时间
    /// </summary>
    public DateTime StartTime { get; set; } = DateTime.Now;

    /// <summary>
    ///     用于日志显示的事件内容
    /// </summary>
    /// <returns>事件内容</returns>
    public string Message => $"应用启动引导开始，开始时间： {StartTime}";
}