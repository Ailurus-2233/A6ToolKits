using Avalonia.Controls;

namespace A6ToolKits.UIPackage.Controls.Layout.Page;

/// <summary>
///     页面容器，用于管理多个页面
/// </summary>
public class PageContainer : ContentControl
{
    private int _currentPageIndex = -1;
    private List<ContentControl> Pages { get; } = [];
    private Dictionary<string, ContentControl> PageDictionary { get; } = new();

    /// <summary>
    ///     是否可以移动到上一页
    /// </summary>
    public bool CanMoveToPreviousPage => PreviousPages.Count > 0;

    /// <summary>
    ///     是否可以移动到下一页
    /// </summary>
    public bool CanMoveToNextPage => NextPages.Count > 0;
    
    /// <summary>
    ///     当前活动的页面
    /// </summary>
    public ContentControl CurrentPage
    {
        get => Pages[_currentPageIndex];
        set
        {
            var index = Pages.IndexOf(value);
            if (index != -1)
            {
                MoveToPage(index);
            }
            else
            {
                Pages.Add(value);
                MoveToPage(Pages.Count - 1);
            }

            PreviousPages.Push(Pages[_currentPageIndex]);
            NextPages.Clear();
        }
    }

    /// <summary>
    ///     默认页面索引
    /// </summary>
    public int DefaultPageIndex { get; set; } = 0;

    /// <summary>
    ///     默认页面
    /// </summary>
    public ContentControl DefaultPage => Pages[DefaultPageIndex];

    /// <summary>
    ///     返回的数据栈
    /// </summary>
    private Stack<ContentControl> PreviousPages { get; } = new();

    /// <summary>
    ///     前进的数据栈
    /// </summary>
    private Stack<ContentControl> NextPages { get; } = new();

    /// <summary>
    ///     页面切换事件
    /// </summary>
    public event EventHandler? PageChanged;

    /// <summary>
    ///     移动到指定页面
    /// </summary>
    /// <param name="index">
    ///     页面索引
    /// </param>
    /// <exception cref="Exception">
    ///     索引超出范围
    /// </exception>
    public void MoveToPage(int index)
    {
        if (index < 0 || index >= Pages.Count)
            throw new Exception("Index out of range.");

        _currentPageIndex = index;
        var page = Pages[_currentPageIndex];
        Content = page;
        DataContext = page.DataContext;
        PageChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    ///     移动到上一页
    /// </summary>
    /// <exception cref="Exception">
    ///     上一页未找到
    /// </exception>
    public void MoveToPreviousPage()
    {
        if (PreviousPages.Count == 0)
            return;

        var previousPage = PreviousPages.Pop();
        var index = Pages.IndexOf(previousPage);
        if (index != -1)
        {
            NextPages.Push(CurrentPage);
            MoveToPage(index);
        }
        else
        {
            throw new Exception("Previous page not found.");
        }
    }

    /// <summary>
    ///     移动到下一页
    /// </summary>
    /// <exception cref="Exception">
    ///     下一页未找到
    /// </exception>
    public void MoveToNextPage()
    {
        if (NextPages.Count == 0)
            return;

        var nextPage = NextPages.Pop();
        var index = Pages.IndexOf(nextPage);
        if (index != -1)
        {
            PreviousPages.Push(CurrentPage);
            MoveToPage(index);
        }
        else
        {
            throw new Exception("Next page not found.");
        }
    }

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
        Pages.Add(page);
        if (string.IsNullOrEmpty(name)) return;
        PageDictionary.TryAdd(name, page);
    }

    /// <summary>
    ///     指定控件激活页面，如果页面不存在则添加到容器中
    /// </summary>
    /// <param name="page">
    ///     页面控件
    /// </param>
    public void ActivatePage(ContentControl page)
    {
        var index = Pages.IndexOf(page);

        if (index == _currentPageIndex)
            return;

        if (_currentPageIndex != -1)
            PreviousPages.Push(Pages[_currentPageIndex]);

        NextPages.Clear();

        if (index != -1)
        {
            MoveToPage(index);
        }
        else
        {
            Pages.Add(page);
            MoveToPage(Pages.Count - 1);
        }
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