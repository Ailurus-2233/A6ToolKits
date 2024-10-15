using A6ToolKits.Layout.Definer.Interfaces;
using Avalonia.Controls;

namespace A6ToolKits.Layout.Definer;

public abstract class PageDefiner : IDefiner<Grid>
{
    public abstract UserControl? LeftPanel { get; set; }
    public abstract UserControl? RightPanel { get; set; }
    public abstract UserControl? BottomPanel { get; set; }
    public abstract UserControl Main { get; set; }

    public LayoutAlignment Alignment { get; set; } = LayoutAlignment.Right;

    private readonly Grid _page = new();
    private readonly GridSplitter _leftSplitter = new();
    private readonly GridSplitter _rightSplitter = new();
    private readonly GridSplitter _bottomSplitter = new();
    
    protected double LeftWidth { get; set; } = 0;
    protected double RightWidth { get; set; } = 0;
    protected double BottomHeight { get; set; } = 0;

    public Grid Build()
    {
        if (LeftPanel != null)
        {
            _page.Children.Add(LeftPanel);
            _page.Children.Add(_leftSplitter);
        }

        if (RightPanel != null)
        {
            _page.Children.Add(RightPanel);
            _page.Children.Add(_rightSplitter);
        }

        if (BottomPanel != null)
        {
            _page.Children.Add(BottomPanel);
            _page.Children.Add(_bottomSplitter);
        }

        _page.Children.Add(Main);

        UpdatePageLayout();
        return _page;
    }

    public void UpdatePageLayout()
    {
        var leftStr = LeftWidth > 0 ? $"{LeftWidth}" : "Auto";
        var rightStr = RightWidth > 0 ? $"{RightWidth}" : "Auto";
        var bottomStr = BottomHeight > 0 ? $"{BottomHeight}" : "Auto";
        
        _page.ColumnDefinitions = new ColumnDefinitions($"{leftStr},Auto,*,Auto,{rightStr}");
        _page.RowDefinitions = new RowDefinitions($"*,Auto,{bottomStr}");
        
        switch (Alignment)
        {
            case LayoutAlignment.Left:
                SetLeftAlignmentLayout();
                break;
            case LayoutAlignment.Center:
                SetCenterAlignmentLayout();
                break;
            case LayoutAlignment.TowSide:
                SetTowSideAlignmentLayout();
                break;
            case LayoutAlignment.Right:
            default:
                SetRightAlignmentLayout();
                break;
        }
    }

    private void SetLeftAlignmentLayout()
    {
        LeftPanel?.SetGridPosition(0, 0, 2, 2);
        _leftSplitter.SetGridPosition(0, 1, 2, 1);
        Main.SetGridPosition(0, 2, 2, 2);
        _rightSplitter.SetGridPosition(0, 3, 3, 1);
        RightPanel?.SetGridPosition(0, 4, 3, 1);
        _bottomSplitter.SetGridPosition(1, 0, 1, 4);
        BottomPanel?.SetGridPosition(2, 0, 1, 4);
    }

    private void SetRightAlignmentLayout()
    {
        LeftPanel?.SetGridPosition(0, 0, 3, 2);
        Main.SetGridPosition(0, 2, 2, 2);
        RightPanel?.SetGridPosition(0, 4, 2);
        BottomPanel?.SetGridPosition(2, 2, 1, 4);

        _leftSplitter.SetGridPosition(0, 1, 3);
        _rightSplitter.SetGridPosition(0, 3, 2);
        _bottomSplitter.SetGridPosition(1, 2, 1, 4);
    }

    private void SetCenterAlignmentLayout()
    {
        LeftPanel?.SetGridPosition(0, 0, 3, 2);
        Main.SetGridPosition(0, 2, 2, 2);
        RightPanel?.SetGridPosition(0, 4, 3);
        BottomPanel?.SetGridPosition(2, 2, 1, 2);

        _leftSplitter.SetGridPosition(0, 1, 3);
        _rightSplitter.SetGridPosition(0, 3, 3);
        _bottomSplitter.SetGridPosition(1, 2, 1, 2);
    }

    private void SetTowSideAlignmentLayout()
    {
        LeftPanel?.SetGridPosition(0, 0, 2, 2);
        Main.SetGridPosition(0, 2, 2, 2);
        RightPanel?.SetGridPosition(0, 4, 2);
        BottomPanel?.SetGridPosition(2, 0, 1, 5);

        _leftSplitter.SetGridPosition(0, 1, 2);
        _rightSplitter.SetGridPosition(0, 3, 2);
        _bottomSplitter.SetGridPosition(1, 0, 1, 5);
    }
}

internal static class ControlExtension
{
    internal static void SetGridPosition(this Control control, int row, int column, int rowSpan = 1, int columnSpan = 1)
    {
        control.SetValue(Grid.RowProperty, row);
        control.SetValue(Grid.ColumnProperty, column);
        control.SetValue(Grid.RowSpanProperty, rowSpan);
        control.SetValue(Grid.ColumnSpanProperty, columnSpan);
    }
}

public enum LayoutAlignment
{
    /// <summary>
    /// 左对齐
    /// </summary>
    Left,

    /// <summary>
    /// 右对齐
    /// </summary>
    Right,

    /// <summary>
    /// 中心对齐
    /// </summary>
    Center,

    /// <summary>
    /// 两边对齐
    /// </summary>
    TowSide
}