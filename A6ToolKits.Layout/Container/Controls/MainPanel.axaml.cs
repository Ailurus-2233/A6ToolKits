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
    ///     构造函数，初始化主面板
    /// </summary>
    public MainPanel()
    {
        InitializeComponent();
    }

    private enum Orientation
    {
        Horizontal,
        Vertical
    }

    public void SetLayout(LayoutAlignment alignment, int leftWidth = 300, int rightWidth = 300, int bottomHeight = 300)
    {
        MainGrid.ColumnDefinitions = new ColumnDefinitions($"{leftWidth},Auto,*,Auto,{rightWidth}");
        MainGrid.RowDefinitions = new RowDefinitions($"*,Auto,{bottomHeight}");

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

    private void SetLeftAlignmentLayout()
    {
        LeftItemBar.SetValue(Grid.RowProperty, 0);
        LeftItemBar.SetValue(Grid.ColumnProperty, 0);

        LeftSplitter.SetValue(Grid.RowProperty, 0);
        LeftSplitter.SetValue(Grid.ColumnProperty, 1);

        PageContainer.SetValue(Grid.RowProperty, 0);
        PageContainer.SetValue(Grid.ColumnProperty, 2);

        BottomSplitter.SetValue(Grid.RowProperty, 1);
        BottomSplitter.SetValue(Grid.ColumnProperty, 0);
        BottomSplitter.SetValue(Grid.ColumnSpanProperty, 3);

        BottomItemBar.SetValue(Grid.RowProperty, 2);
        BottomItemBar.SetValue(Grid.ColumnProperty, 0);
        BottomItemBar.SetValue(Grid.ColumnSpanProperty, 3);

        RightSplitter.SetValue(Grid.RowProperty, 0);
        RightSplitter.SetValue(Grid.ColumnProperty, 3);
        RightSplitter.SetValue(Grid.RowSpanProperty, 3);

        RightItemBar.SetValue(Grid.RowProperty, 0);
        RightItemBar.SetValue(Grid.ColumnProperty, 4);
        RightItemBar.SetValue(Grid.RowSpanProperty, 3);
    }

    private void SetRightAlignmentLayout()
    {
        RightItemBar.SetValue(Grid.RowProperty, 0);
        RightItemBar.SetValue(Grid.ColumnProperty, 4);

        RightSplitter.SetValue(Grid.RowProperty, 0);
        RightSplitter.SetValue(Grid.ColumnProperty, 3);

        PageContainer.SetValue(Grid.RowProperty, 0);
        PageContainer.SetValue(Grid.ColumnProperty, 2);

        BottomSplitter.SetValue(Grid.RowProperty, 1);
        BottomSplitter.SetValue(Grid.ColumnProperty, 2);
        BottomSplitter.SetValue(Grid.ColumnSpanProperty, 3);

        BottomItemBar.SetValue(Grid.RowProperty, 2);
        BottomItemBar.SetValue(Grid.ColumnProperty, 2);
        BottomItemBar.SetValue(Grid.ColumnSpanProperty, 3);

        LeftSplitter.SetValue(Grid.RowProperty, 0);
        LeftSplitter.SetValue(Grid.ColumnProperty, 1);
        LeftSplitter.SetValue(Grid.RowSpanProperty, 3);

        LeftItemBar.SetValue(Grid.RowProperty, 0);
        LeftItemBar.SetValue(Grid.ColumnProperty, 0);
        LeftItemBar.SetValue(Grid.RowSpanProperty, 3);
    }

    private void SetCenterAlignmentLayout()
    {
        PageContainer.SetValue(Grid.RowProperty, 0);
        PageContainer.SetValue(Grid.ColumnProperty, 2);

        BottomSplitter.SetValue(Grid.RowProperty, 1);
        BottomSplitter.SetValue(Grid.ColumnProperty, 2);

        BottomItemBar.SetValue(Grid.RowProperty, 2);
        BottomItemBar.SetValue(Grid.ColumnProperty, 2);

        LeftSplitter.SetValue(Grid.RowProperty, 0);
        LeftSplitter.SetValue(Grid.ColumnProperty, 1);
        LeftSplitter.SetValue(Grid.RowSpanProperty, 3);

        LeftItemBar.SetValue(Grid.RowProperty, 0);
        LeftItemBar.SetValue(Grid.ColumnProperty, 0);
        LeftItemBar.SetValue(Grid.RowSpanProperty, 3);

        RightSplitter.SetValue(Grid.RowProperty, 0);
        RightSplitter.SetValue(Grid.ColumnProperty, 3);
        RightSplitter.SetValue(Grid.RowSpanProperty, 3);

        RightItemBar.SetValue(Grid.RowProperty, 0);
        RightItemBar.SetValue(Grid.ColumnProperty, 4);
        RightItemBar.SetValue(Grid.RowSpanProperty, 3);
    }

    private void SetTowSideAlignmentLayout()
    {
        LeftItemBar.SetValue(Grid.RowProperty, 0);
        LeftItemBar.SetValue(Grid.ColumnProperty, 0);

        LeftSplitter.SetValue(Grid.RowProperty, 0);
        LeftSplitter.SetValue(Grid.ColumnProperty, 1);

        PageContainer.SetValue(Grid.RowProperty, 0);
        PageContainer.SetValue(Grid.ColumnProperty, 2);

        RightSplitter.SetValue(Grid.RowProperty, 0);
        RightSplitter.SetValue(Grid.ColumnProperty, 3);

        RightItemBar.SetValue(Grid.RowProperty, 0);
        RightItemBar.SetValue(Grid.ColumnProperty, 4);

        BottomSplitter.SetValue(Grid.RowProperty, 1);
        BottomSplitter.SetValue(Grid.ColumnProperty, 0);
        BottomSplitter.SetValue(Grid.ColumnSpanProperty, 5);

        BottomItemBar.SetValue(Grid.RowProperty, 2);
        BottomItemBar.SetValue(Grid.ColumnProperty, 0);
        BottomItemBar.SetValue(Grid.ColumnSpanProperty, 5);
    }
}