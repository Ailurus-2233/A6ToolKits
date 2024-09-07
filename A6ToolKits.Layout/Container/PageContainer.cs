using Avalonia.Controls;

namespace A6ToolKits.Layout.Container;

public class PageContainer : ContentControl
{
    public List<ContentControl> Pages { get; set; } = [];
    public bool CanMoveToPreviousPage => PreviousPages.Count > 0;
    public bool CanMoveToNextPage => NextPages.Count > 0;
    private int _currentPageIndex;


    public ContentControl CurrentPage
    {
        get => Pages[_currentPageIndex];
        set
        {
            var index = Pages.IndexOf(value);
            if (index != -1)
                MoveToPage(index);
            else
            {
                Pages.Add(value);
                MoveToPage(Pages.Count - 1);
            }

            PreviousPages.Push(Pages[_currentPageIndex]);
            NextPages.Clear();
        }
    }

    private int DefaultPageIndex { get; set; } = 0;

    public ContentControl DefaultPage => Pages[DefaultPageIndex];
    private Stack<ContentControl> PreviousPages { get; set; } = new();
    private Stack<ContentControl> NextPages { get; set; } = new();

    private void MoveToPage(int index)
    {
        if (index < 0 || index >= Pages.Count)
            throw new Exception("Index out of range.");

        _currentPageIndex = index;
        var page = Pages[_currentPageIndex] as ContentControl;
        Content = page;
        DataContext = page?.DataContext;
    }

    public void MoveToPreviousPage()
    {
        if (PreviousPages.Count == 0)
            return;

        var previousPage = PreviousPages.Pop();
        var index = Pages.IndexOf(previousPage);
        if (index != -1)
            MoveToPage(index);
        else
            throw new Exception("Previous page not found.");
        NextPages.Push(previousPage);
    }

    public void MoveToNextPage()
    {
        if (NextPages.Count == 0)
            return;

        var nextPage = NextPages.Pop();
        var index = Pages.IndexOf(nextPage);
        if (index != -1)
            MoveToPage(index);
        else
            throw new Exception("Next page not found.");
        PreviousPages.Push(nextPage);
    }
}