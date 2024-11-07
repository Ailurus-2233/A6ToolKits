﻿namespace A6ToolKits.Common.Logger;

/// <summary>
///     日志接口
/// </summary>
public interface ILogger
{
    /// <summary>
    ///     记录调试信息
    /// </summary>
    /// <param name="message">消息</param>
    void Debug(string message);

    /// <summary>
    ///     记录信息
    /// </summary>
    /// <param name="message">消息</param>
    void Info(string message);

    /// <summary>
    ///     记录警告
    /// </summary>
    /// <param name="message">消息</param>
    void Warn(string message);

    /// <summary>
    ///     记录错误
    /// </summary>
    /// <param name="message">消息</param>
    void Error(string message);

    /// <summary>
    ///     记录错误
    /// </summary>
    /// <param name="message">消息</param>
    /// <param name="exception">异常</param>
    void Error(string message, Exception exception);

    /// <summary>
    ///     记录严重错误
    /// </summary>
    /// <param name="message">消息</param>
    void Fatal(string message);

    /// <summary>
    ///     记录严重错误
    /// </summary>
    /// <param name="message">消息</param>
    /// <param name="exception">异常</param>
    void Fatal(string message, Exception exception);
}