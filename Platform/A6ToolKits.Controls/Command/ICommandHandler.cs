namespace A6ToolKits.Controls.Command;

/// <summary>
///     Command 处理器接口
/// </summary>
public interface ICommandHandler
{
    /// <summary>
    ///     更新 Command
    /// </summary>
    void Update();

    /// <summary>
    ///     异步运行 Command
    /// </summary>
    /// <returns>
    ///     返回一个异步任务
    /// </returns>
    Task Run();
}