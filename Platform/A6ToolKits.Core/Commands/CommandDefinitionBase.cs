using A6ToolKits.Common;
using Avalonia.Media;

namespace A6ToolKits.Commands;

/// <summary>
///     Command 属性定义基类
/// </summary>
public abstract class CommandDefinitionBase : ViewModelBase
{
    /// <summary>
    ///     Command 名称
    /// </summary>
    public abstract string? Name { get; }

    /// <summary>
    ///     Command 提示
    /// </summary>
    public abstract string? ToolTip { get; }

    /// <summary>
    ///     Command 图标
    /// </summary>
    public abstract IImage Image { get; }
}