using Avalonia.Controls;

namespace A6ToolKits.Layout.Container;

/// <summary>
///    容器接口，用于管理多子项的视图
/// </summary>
public interface IContainer<T> where T : IContainerItem
{
    /// <summary>
    ///     子项集合
    /// </summary>
    public List<T> Items { get; set; }

    /// <summary>
    ///     当前选中的子项
    /// </summary>
    public IContainerItem SelectedItem { get; set; }
    
    /// <summary>
    ///     获取用于显示标签集合的控件
    /// </summary>
    /// <returns></returns>
    public ContentControl GetTabsControl();
    
    /// <summary>
    ///     添加子项
    /// </summary>
    /// <param name="item">
    ///     操作的子项
    /// </param>
    public void AddItem(T item);
    
    /// <summary>
    ///     移除子项
    /// </summary>
    /// <param name="item">
    ///     操作的子项
    /// </param>
    public void RemoveItem(T item);
    
    /// <summary>
    ///     选中子项
    /// </summary>
    /// <param name="item">
    ///     操作的子项
    /// </param>
    public void SelectItem(T item);
}