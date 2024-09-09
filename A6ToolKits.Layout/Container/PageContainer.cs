using Avalonia.Controls;

namespace A6ToolKits.Layout.Container;

public class PageContainer : ContentControl
{
    private int _currentPageIndex = -1;
    private List<ContentControl> Pages { get; set; } = [];
    private Dictionary<string, ContentControl> PageDictionary { get; set; } = new();
    public bool CanMoveToPreviousPage => PreviousPages.Count > 0;
    public bool CanMoveToNextPage => NextPages.Count > 0;

    public event EventHandler? PageChanged;


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

    private int DefaultPageIndex { get; } = 0;

    public ContentControl DefaultPage => Pages[DefaultPageIndex];
    private Stack<ContentControl> PreviousPages { get; } = new();
    private Stack<ContentControl> NextPages { get; } = new();

    public void MoveToPage(int index)
    {
        if (index < 0 || index >= Pages.Count)
            throw new Exception("Index out of range.");

        _currentPageIndex = index;
        var page = Pages[_currentPageIndex];
        Content = page;
        DataContext = page.DataContext;
        OnPageChanged();
    }

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
            throw new Exception("Previous page not found.");
    }

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
            throw new Exception("Next page not found.");
    }

    private void OnPageChanged()
    {
        PageChanged?.Invoke(this, EventArgs.Empty);
    }

    public void AddPage(string name, ContentControl page)
    {
        Pages.Add(page);
        if (string.IsNullOrEmpty(name)) return;
        PageDictionary.TryAdd(name, page);
    }

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

    public void ActivatePage(string name)
    {
        if (PageDictionary.TryGetValue(name, out var page))
        {
            ActivatePage(page);
        }
    }
}