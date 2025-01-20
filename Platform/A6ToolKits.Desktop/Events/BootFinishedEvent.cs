namespace A6ToolKits.Events;

/// <summary>
///     启动完成事件
/// </summary>
public class BootFinishedEvent : EventBase
{
    /// <summary>
    ///     应用启动引导结束时间
    /// </summary>
    public DateTime FinishedDateTime { get; set; } = DateTime.Now;


    /// <inheritdoc />
    public override string Message => $"应用启动引导结束，完成时间：{FinishedDateTime:yyyy-MM-dd HH:mm:ss}";
}