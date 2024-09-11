using System;

namespace A6ToolKits.Attributes;

/// <summary>
///     状态栏属性，标记到一个 UserControl 的属性上，用于生成 StatusBar
/// </summary>
/// <param name="position"></param>
[AttributeUsage(AttributeTargets.Property)]
public class StatusBarAttribute(string position) : Attribute
{
    /// <summary>
    ///     状态栏位置，生成 StatusBar 时，根据位置将控件放置到对应的位置
    /// </summary>
    public StatusPosition Position { get; set; } = Enum.Parse<StatusPosition>(position);
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
    ///     状态栏中间
    /// </summary>
    Center,

    /// <summary>
    ///     状态栏右侧
    /// </summary>
    Right
}