using A6ToolKits.Helper.Config;

namespace A6ToolKits.Layout.Helper;

/// <summary>
///     布局项配置项，包含子类 Window, Menu, ButtonBar, Pages, Items, StatusBar
/// </summary>
public class LayoutItemConfigItem : ConfigItemBase
{
    #region 多种生效的属性

    /// <summary>
    ///     控件加载的程序集，Window，Menu, ButtonBar, Page, StatusBar, Items
    /// </summary>
    public string Assembly { get; set; } = string.Empty;

    /// <summary>
    ///     控件的具体类，Window，Menu, ButtonBar, Page, StatusBar, Items
    /// </summary>
    public string Target { get; set; } = string.Empty;

    /// <summary>
    ///     显示高度 Window，Menu, ButtonBar, StatusBar, Items
    /// </summary>
    public string Height { get; set; } = string.Empty;

    /// <summary>
    ///     显示宽度 Window, Items
    /// </summary>
    public string Width { get; set; } = string.Empty;

    #endregion

    #region Window 生效的属性

    /// <summary>
    ///     窗口类型
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    ///     主题颜色
    /// </summary>
    public string BackgroundColor { get; set; } = "#181818";
    
    /// <summary>
    /// 
    /// </summary>
    public string PrimaryColor { get; set; } = "#3377D2";
    
    /// <summary>
    ///     图标路径
    /// </summary>
    public string IconPath { get; set; } = string.Empty;

    /// <summary>
    ///     对齐方式
    /// </summary>
    public string Alignment { get; set; } = "Right";
    
    /// <summary>
    ///     是否显示工具栏
    /// </summary>
    public string ShowToolBar { get; set; } = "True";
    
    
    /// <summary>
    ///     页面名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    #endregion

    #region Page 生效的属性

    /// <summary>
    ///     默认页面，对 Pages 有效
    /// </summary>
    public string Default { get; set; } = string.Empty;

    #endregion

    #region Items 生效的属性

    /// <summary>
    ///     控件位置，对 Items 有效
    /// </summary>
    public string Position { get; set; } = string.Empty;

    #endregion
}