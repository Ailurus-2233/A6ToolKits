namespace A6ToolKits.Base.Logger;

/// <summary>
///     日志基类
/// </summary>
public abstract class LoggerBase : ILogger
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public abstract void Debug(string message);
    public abstract void Info(string message);
    public abstract void Warn(string message);
    public abstract void Error(string message);
    public abstract void Error(string message, Exception exception);
    public abstract void Fatal(string message);
    public abstract void Fatal(string message, Exception exception);
#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}