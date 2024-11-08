namespace A6ToolKits.Common.Attributes;

/// <summary>
///     工具栏属性，标记到一个 ActionBase 的属性上，用于生成 ToolBar 中的按钮
/// </summary>
/// <param name="group">
///     分组，生成 ToolBar 时，根据分组将按钮放置到对应的位置
/// </param>
/// <param name="type">
///     类型，生成 ToolBar 时，对应按钮的类型
/// </param>
/// <param name="position">
///     位置，生成 ToolBar 时，根据位置将按钮放置到对应的位置，默认为左侧
///     TODO 目前只能在左侧放置，右侧暂未实现
/// </param>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ToolBarActionAttribute(int group, ButtonType type, ToolBarPosition position = ToolBarPosition.Left)
    : Attribute
{
    /// <summary>
    ///     分组，生成 ToolBar 时，根据分组将按钮放置到对应的位置
    /// </summary>
    public int Group { get; set; } = group;

    /// <summary>
    ///     位置，生成 ToolBar 时，根据位置将按钮放置到对应的位置
    /// </summary>
    public ToolBarPosition Position { get; set; } = position;

    /// <summary>
    ///     类型，生成 ToolBar 时，对应按钮的类型
    /// </summary>
    public ButtonType Type { get; set; } = type;
}

/// <summary>
///     工具栏位置
/// </summary>
public enum ToolBarPosition
{
    /// <summary>
    ///     工具栏左侧
    /// </summary>
    Left,

    /// <summary>
    ///     工具栏右侧
    /// </summary>
    Right
}

/// <summary>
///     按钮类型
/// </summary>
public enum ButtonType
{
    /// <summary>
    ///     仅图标
    /// </summary>
    Icon,

    /// <summary>
    ///     仅文字
    /// </summary>
    Text,

    /// <summary>
    ///     图标和文字
    /// </summary>
    IconAndText
}