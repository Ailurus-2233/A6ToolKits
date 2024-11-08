namespace A6ToolKits.Common.Attributes;

/// <summary>
///     状态栏属性，标记到一个 UserControl 的属性上，用于生成 StatusBar
/// </summary>
/// <param name="position"></param>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class StatusBarItemAttribute(StatusPosition position, int order) : Attribute
{
    /// <summary>
    ///     状态栏位置，生成 StatusBar 时，根据位置将控件放置到对应的位置
    /// </summary>
    public StatusPosition Position { get; set; } = position;

    /// <summary>
    ///     序列
    /// </summary>
    public int Order { get; set; } = order;
}

/// <summary>
///     状态栏位置
/// </summary>
public enum StatusPosition
{
    /// <summary>
    ///     状态栏左侧
    /// </summary>
    Left,

    /// <summary>
    ///     状态栏右侧
    /// </summary>
    Right,

    /// <summary>
    ///     状态栏中间
    /// </summary>
    Center
}