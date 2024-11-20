﻿using A6ToolKits.Common;

namespace A6ToolKits.Bootstrapper.Events;

/// <summary>
///     启动完成事件
/// </summary>
public class BootFinishedEvent : EventBase
{
    /// <summary>
    ///     应用启动引导结束时间
    /// </summary>
    public DateTime FinishedDateTime { get; set; } = DateTime.Now;

    /// <summary>
    ///     用于日志显示的事件内容
    /// </summary>
    public override string Message => $"应用启动引导结束，完成时间：{FinishedDateTime}";
}