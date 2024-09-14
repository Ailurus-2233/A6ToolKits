using Avalonia.Controls;
using Avalonia.Media;

namespace A6ToolKits.Layout.Container;

/// <summary>
///     基本的页面项
/// </summary>
public class BasePageItem : IContainerItem
{
    /// <summary>
    ///     页面的标题
    /// </summary>
    public required string Title { get; set; }
    
    /// <summary>
    ///     当鼠标悬浮于 DisplayTab 上时显示的提示
    /// </summary>
    public string? ToolTip { get; set; }
    
    /// <summary>
    ///     页面的图标
    /// </summary>
    public IImage? Icon { get; set; }

    /// <summary>
    ///     页面是否激活
    /// </summary>
    public bool IsActivate { get; set; } = false;
    
    /// <summary>
    ///     页面的标签
    /// </summary>
    public Control Tab => throw new NotImplementedException();


    /// <summary>
    ///     页面的内容
    /// </summary>
    public ContentControl Content { get; set; }
}