// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable VirtualMemberNeverOverridden.Global

using A6ToolKits.Command;

namespace A6ToolKits.Controls.Command;

/// <summary>
/// </summary>
public abstract class CommandBase : CommandDefinitionBase, ICommandHandler
{
    private bool _enable = true;
    private bool _visible = true;

    /// <summary>
    ///     是否可见
    /// </summary>
    public bool Visible
    {
        get => _visible;
        set => SetField(ref _visible, value);
    }

    /// <summary>
    ///     是否可用
    /// </summary>
    public bool Enable
    {
        get => _enable;
        set => SetField(ref _enable, value);
    }

    /// <summary>
    ///     更新 Command
    /// </summary>
    public void Update()
    {
    }

    /// <summary>
    ///     异步运行 Command
    /// </summary>
    /// <returns>
    ///     返回一个异步任务
    /// </returns>
    public abstract Task Run();
}