using A6ToolKits.Events;

namespace A6ToolKits.Starter.Events;

/// <summary>
///     应用程序退出事件
/// </summary>
public class ApplicationExitEvent: EventBase
{
    /// <summary>
    ///     应用程序退出时间
    /// </summary>
    public DateTime ApplicationExit { get; set; } = DateTime.Now;
    
    /// <inheritdoc />
    public override string Message => $"应用程序退出，退出时间：{ApplicationExit:yyyy-MM-dd HH:mm:ss}";
}