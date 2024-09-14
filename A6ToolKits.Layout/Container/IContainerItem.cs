using Avalonia.Controls;
using Avalonia.Media;

namespace A6ToolKits.Layout.Container;

/// <summary>
///     基本容器项的接口
/// </summary>
public interface IContainerItem
{
    /// <summary>
    ///     标题
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    ///     当鼠标悬浮于 DisplayTab 上时显示的提示
    /// </summary>
    public string? ToolTip { get; set; }
    
    /// <summary>
    ///     图标
    /// </summary>
    public IImage? Icon { get; set; }
    
    /// <summary>
    ///     是否激活
    /// </summary>
    public bool IsActivate { get; set; }
    
    /// <summary>
    ///     标签控件
    /// </summary>
    public Control Tab { get; }
    
    /// <summary>
    ///     内容
    /// </summary>
    public ContentControl Content { get; set; }
}