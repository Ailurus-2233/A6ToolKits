using A6ToolKits.Common;

namespace A6ToolKits.Bootstrapper.Events;

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