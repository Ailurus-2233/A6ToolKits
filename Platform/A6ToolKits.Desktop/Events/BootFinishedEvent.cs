﻿namespace A6ToolKits.Events;

/// <summary>
///     启动完成事件
/// </summary>
public class BootFinishedEvent : EventBase
{
    /// <inheritdoc />
    public override string Message => $"应用启动引导结束，完成时间：{Time:yyyy-MM-dd HH:mm:ss}";
}