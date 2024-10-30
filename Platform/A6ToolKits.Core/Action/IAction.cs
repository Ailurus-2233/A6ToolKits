using System;
using System.Threading.Tasks;

namespace A6ToolKits.Action;

/// <summary>
///     动作接口，可以异步执行一个操作
/// </summary>
public interface IAction
{
    /// <summary>
    ///     执行动作
    /// </summary>
    /// <returns>
    ///     返回一个异步任务
    /// </returns>
    public Task Run();
}