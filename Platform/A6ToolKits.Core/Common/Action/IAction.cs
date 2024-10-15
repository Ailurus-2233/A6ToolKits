using System;
using System.Threading.Tasks;

namespace A6ToolKits.Common.Action;

/// <summary>
///     动作接口，可以异步执行一个操作
/// </summary>
public interface IAction
{
    /// <summary>
    ///     是否可以执行
    /// </summary>
    public bool CanRun { get; set; }

    /// <summary>
    ///     动作是否可以执行发生变化时触发的事件
    /// </summary>
    public EventHandler? CanRunChanged { get; set; }

    /// <summary>
    ///     执行动作
    /// </summary>
    /// <returns>
    ///     返回一个异步任务
    /// </returns>
    public Task Run();
}