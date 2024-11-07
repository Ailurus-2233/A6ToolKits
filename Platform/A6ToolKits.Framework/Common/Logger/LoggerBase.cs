namespace A6ToolKits.Common.Logger;

public abstract class LoggerBase : ILogger
{
    public abstract void Debug(string message);
    public abstract void Info(string message);
    public abstract void Warn(string message);
    public abstract void Error(string message);
    public abstract void Error(string message, Exception exception);
    public abstract void Fatal(string message);
    public abstract void Fatal(string message, Exception exception);
}