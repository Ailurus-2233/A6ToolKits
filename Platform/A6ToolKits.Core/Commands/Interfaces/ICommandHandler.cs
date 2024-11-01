using System.Threading.Tasks;

namespace A6ToolKits.Commands.Interfaces;

/// <summary>
///     Command 处理器接口
/// </summary>
public interface ICommandHandler<TCommandDefinition>: ICommandHandler where TCommandDefinition : CommandDefinitionBase
{
    
}

/// <summary>
///     Command 处理器接口
/// </summary>
public interface ICommandHandler
{
    /// <summary>
    ///     更新 Command
    /// </summary>
    /// <param name="commandControlItem">
    ///     Command 对象
    /// </param>
    void Update(CommandControlItem commandControlItem);
    
    /// <summary>
    ///     异步运行 Command
    /// </summary>
    /// <param name="commandControlItem">
    ///     Command 对象
    /// </param>
    /// <returns>
    ///     返回一个异步任务
    /// </returns>
    Task Run(CommandControlItem commandControlItem);
}