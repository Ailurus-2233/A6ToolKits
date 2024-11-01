using System.Threading.Tasks;
using A6ToolKits.Commands.Interfaces;

namespace A6ToolKits.Commands;

/// <summary>
///     Command 处理器基类
/// </summary>
/// <typeparam name="TCommandDefinition">
///     Command 定义类型
/// </typeparam>
public abstract class CommandHandlerBase<TCommandDefinition> : ICommandHandler<TCommandDefinition>
    where TCommandDefinition : CommandDefinitionBase
{
    /// <summary>
    ///     更新 Command 状态
    /// </summary>
    /// <param name="commandControlItem">
    ///     Command 对象
    /// </param>
    public virtual void Update(CommandControlItem commandControlItem)
    {
        
    }

    /// <summary>
    ///     异步运行 Command
    /// </summary>
    /// <param name="commandControlItem">
    ///     Command 对象
    /// </param>
    /// <returns>
    ///     返回一个异步任务
    /// </returns>
    public abstract Task Run(CommandControlItem commandControlItem);
}