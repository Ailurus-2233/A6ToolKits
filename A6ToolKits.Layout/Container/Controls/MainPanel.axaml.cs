using A6ToolKits.Layout.Container.Controls.Enums;
using A6ToolKits.Layout.Container.Controls.ItemBar;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DynamicData;

namespace A6ToolKits.Layout.Container.Controls;

/// <summary>
///    主面板，用于管理主要的布局，包括左侧、右侧、顶部、底部、页面
/// </summary>
public partial class MainPanel : UserControl
{
    /// <summary>
    ///     辅助侧边栏，用于展示一些信息，位置在窗口右侧
    /// </summary>
    public RightItemBar RightItemBar { get; } = new();

    /// <summary>
    ///     主侧边栏，用于展示一些信息，位置在窗口左侧
    /// </summary>
    public LeftItemBar LeftItemBar { get; } = new();

    /// <summary>
    ///     底部侧边栏，用于展示一些信息，位置在窗口底部
    /// </summary>
    public BottomItemBar BottomItemBar { get; } = new();

    /// <summary>
    ///     页面容器，用于展示页面内容
    /// </summary>
    public PageContainer PageContainer { get; } = new();

    /// <summary>
    ///     构造函数，初始化主面板
    /// </summary>
    public MainPanel(LayoutAlignment alignment)
    {
        InitializeComponent();
        switch (alignment)
        {
            case LayoutAlignment.Left:
                SetLeftAlignmentLayout();
                break;
            case LayoutAlignment.TowSide:
                SetTowSideAlignmentLayout();
                break;
            case LayoutAlignment.Center:
                SetCenterAlignmentLayout();
                break;
            case LayoutAlignment.Right:
            default:
                SetRightAlignmentLayout();
                break;
        }
    }

    private enum Orientation
    {
        Horizontal,
        Vertical
    }

    private static void SetGridPanel(Grid grid, Orientation orientation, Control control1, Control control2)
    {
        var gridSplitter = new GridSplitter();
        var property = orientation == Orientation.Horizontal ? Grid.ColumnProperty : Grid.RowProperty;

        switch (orientation)
        {
            case Orientation.Horizontal:
                grid.RowDefinitions = null!;
                grid.ColumnDefinitions = new ColumnDefinitions("Auto,Auto,Auto");
                break;
            case Orientation.Vertical:
            default:
                grid.ColumnDefinitions = null!;
                grid.RowDefinitions = new RowDefinitions("Auto,Auto,Auto");
                break;
        }

        grid.Children.Clear();

        control1.SetValue(property, 0);
        gridSplitter.SetValue(property, 1);
        control2.SetValue(property, 2);

        grid.Children.Add(control1);
        grid.Children.Add(gridSplitter);
        grid.Children.Add(control2);
    }


    private void SetLeftAlignmentLayout()
    {
        var topGrid = new Grid();
        var leftGrid = new Grid();

        SetGridPanel(topGrid, Orientation.Horizontal, LeftItemBar, PageContainer);
        SetGridPanel(leftGrid, Orientation.Vertical, topGrid, BottomItemBar);
        SetGridPanel(MainGrid, Orientation.Horizontal, leftGrid, RightItemBar);
    }

    private void SetRightAlignmentLayout()
    {
        var topGrid = new Grid();
        var rightGrid = new Grid();

        SetGridPanel(topGrid, Orientation.Horizontal, PageContainer, RightItemBar);
        SetGridPanel(rightGrid, Orientation.Vertical, topGrid, BottomItemBar);
        SetGridPanel(MainGrid, Orientation.Horizontal, LeftItemBar, rightGrid);
    }

    private void SetCenterAlignmentLayout()
    {
        var centerGrid = new Grid();
        var rightGrid = new Grid();

        SetGridPanel(centerGrid, Orientation.Vertical, PageContainer, BottomItemBar);
        SetGridPanel(rightGrid, Orientation.Horizontal, centerGrid, RightItemBar);
        SetGridPanel(MainGrid, Orientation.Horizontal, LeftItemBar, rightGrid);
    }

    private void SetTowSideAlignmentLayout()
    {
        var rightGrid = new Grid();
        var topGrid = new Grid();

        SetGridPanel(rightGrid, Orientation.Horizontal, PageContainer, RightItemBar);
        SetGridPanel(topGrid, Orientation.Horizontal, LeftItemBar, rightGrid);
        SetGridPanel(MainGrid, Orientation.Vertical, topGrid, BottomItemBar);
    }
}