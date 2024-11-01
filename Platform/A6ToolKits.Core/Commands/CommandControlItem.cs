using System;
using A6ToolKits.Common;
using A6ToolKits.Resource;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace A6ToolKits.Commands;

/// <summary>
///     Command 基类
/// </summary>
public sealed class CommandControlItem : PropertyChangedBase
{
    private bool _visible = true;
    private bool _enable = true;
    private string? _name;
    private string? _toolTip;
    private Uri? _iconSource;
    private IImage? _image;

    /// <summary>
    ///     Command 定义类
    /// </summary>
    public CommandDefinitionBase CommandDefinition { get; }

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
    ///     Command 名称
    /// </summary>
    public string? Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }
    
    /// <summary>
    ///     Command 提示
    /// </summary>
    public string? ToolTip
    {
        get => _toolTip;
        set => SetField(ref _toolTip, value);
    }
    
    /// <summary>
    ///     Command 图标引用地址
    /// </summary>
    public Uri? IconSource
    {
        get => _iconSource;
        set => SetField(ref _iconSource, value);
    }
    
    /// <summary>
    ///     Command 图标
    /// </summary>
    public IImage? Image
    {
        get => _image;
        set => SetField(ref _image, value);
    }

    /// <summary>
    ///     构造函数
    /// </summary>
    /// <param name="commandDefinition">
    ///     Command 定义类
    /// </param>
    public CommandControlItem(CommandDefinitionBase commandDefinition)
    {
        CommandDefinition = commandDefinition;
        Name = commandDefinition.Name;
        ToolTip = commandDefinition.ToolTip;
        IconSource = commandDefinition.IconSource;
        Image = commandDefinition.Image;
        if (Image is null && IconSource is not null)
            Image = AssertHelper.LoadImage(IconSource);
    }
}