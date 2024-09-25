using A6ToolKits.UIPackage.Controls.Layout.Tab.Models;
using Avalonia.Controls;
using TabItem = A6ToolKits.UIPackage.Controls.Layout.Tab.Models.TabItem;

namespace A6ToolKits.Layout.Controls;

public partial class PageContainer : UserControl
{
    public PageContainer()
    {
        InitializeComponent();
        Page.TabCollection = _pageCollection;
    }
    
    private readonly TabCollection _pageCollection = new TabCollection(groupName:"PageContainer");
    private Dictionary<string, TabItem> PageDictionary { get; } = new();
    
    /// <summary>
    ///     默认页面索引
    /// </summary>
    public string DefaultPageName { get; set; }

    /// <summary>
    ///     默认页面
    /// </summary>
    public TabItem DefaultPage => PageDictionary[DefaultPageName];
    
    
    /// <summary>
    ///     添加页面到容器中
    /// </summary>
    /// <param name="name">
    ///     页面名称
    /// </param>
    /// <param name="page">
    ///     页面控件
    /// </param>
    public void AddPage(string name, ContentControl page)
    {
        var tabItem = new TabItem { Header = name, ToolTip = name, Content = page};
        _pageCollection.AddItem(tabItem);
        PageDictionary.Add(name, tabItem);
    }

    /// <summary>
    ///     指定控件激活页面，如果页面不存在则添加到容器中
    /// </summary>
    /// <param name="page">
    ///     页面控件
    /// </param>
    public void ActivatePage(TabItem page)
    {
        _pageCollection.SelectedItem = page;
    }

    /// <summary>
    ///     指定名称激活页面，如果页面不存在则不操作
    /// </summary>
    /// <param name="name">
    ///     页面名称
    /// </param>
    public void ActivatePage(string name)
    {
        if (PageDictionary.TryGetValue(name, out var page)) ActivatePage(page);
    }
}