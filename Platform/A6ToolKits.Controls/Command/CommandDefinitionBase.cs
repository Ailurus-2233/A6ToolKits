using Avalonia.Media;

namespace A6ToolKits.Controls.Command;

/// <summary>
///     Command 属性定义基类
/// </summary>
public abstract class CommandDefinitionBase : ControlBase
{
    /// <summary>
    ///     Command 名称
    /// </summary>
    public abstract string? Text { get; }

    /// <summary>
    ///     Command 提示
    /// </summary>
    public abstract string? ToolTip { get; }

    /// <summary>
    ///     Command 图标
    /// </summary>
    public abstract IImage? Image { get; }
}