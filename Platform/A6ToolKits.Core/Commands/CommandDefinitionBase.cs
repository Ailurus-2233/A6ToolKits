using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using A6ToolKits.Common;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace A6ToolKits.Commands;

/// <summary>
///     Command 属性定义基类
/// </summary>
public abstract class CommandDefinitionBase : PropertyChangedBase
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