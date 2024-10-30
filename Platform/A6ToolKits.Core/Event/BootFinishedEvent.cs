using System;

namespace A6ToolKits.Event;

/// <summary>
///     启动完成事件
/// </summary>
public class BootFinishedEvent : IEvent
{
    /// <summary>
    ///     应用启动引导结束时间
    /// </summary>
    public DateTime FinishedDateTime { get; set; } = DateTime.Now;

    /// <summary>
    ///     用于日志显示的事件内容
    /// </summary>
    public string Message => $"应用启动引导结束，完成时间：{FinishedDateTime}";
}