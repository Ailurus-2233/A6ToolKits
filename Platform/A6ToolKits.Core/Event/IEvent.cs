﻿namespace A6ToolKits.Event;

/// <summary>
///     事件接口
/// </summary>
public interface IEvent
{
    /// <summary>
    ///     用于日志显示的事件内容
    /// </summary>
    public string Message { get; }
}