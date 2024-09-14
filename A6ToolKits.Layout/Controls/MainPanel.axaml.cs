using A6ToolKits.Layout.Controls.TabBar;
using A6ToolKits.Layout.Enums;
using Avalonia.Controls;

namespace A6ToolKits.Layout.Controls;

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
        ControlExtension.SetGridPosition(LeftItemBar, 0, 0, 2, 2);
        ControlExtension.SetGridPosition(LeftSplitter, 0, 1, 2, 1);
        ControlExtension.SetGridPosition(PageContainer, 0, 2, 2, 2);
        ControlExtension.SetGridPosition(RightItemBar, 0, 3, 3, 1);
        ControlExtension.SetGridPosition(RightItemBar, 0, 4, 3, 1);
        ControlExtension.SetGridPosition(BottomSplitter, 1, 0, 1, 4);
        ControlExtension.SetGridPosition(BottomItemBar, 2, 0, 1, 4);
    }

    private void SetRightAlignmentLayout()
    {
        ControlExtension.SetGridPosition(LeftItemBar, 0, 0, 3, 2);
        ControlExtension.SetGridPosition(PageContainer, 0, 2, 2, 2);
        ControlExtension.SetGridPosition(RightItemBar, 0, 4, 2, 1);
        ControlExtension.SetGridPosition(BottomItemBar, 2, 2, 1, 4);
        
        ControlExtension.SetGridPosition(LeftSplitter, 0, 1, 3, 1);
        ControlExtension.SetGridPosition(RightSplitter, 0, 3, 2, 1);
        ControlExtension.SetGridPosition(BottomSplitter, 1, 2, 1, 4);
    }

    private void SetCenterAlignmentLayout()
    {
        ControlExtension.SetGridPosition(LeftItemBar, 0, 0, 3, 2);
        ControlExtension.SetGridPosition(PageContainer, 0, 2, 2, 2);
        ControlExtension.SetGridPosition(RightItemBar, 0, 4, 3, 1);
        ControlExtension.SetGridPosition(BottomItemBar, 2, 2, 1, 2);
        
        ControlExtension.SetGridPosition(LeftSplitter, 0, 1, 3, 1);
        ControlExtension.SetGridPosition(RightSplitter, 0, 3, 3, 1);
        ControlExtension.SetGridPosition(BottomSplitter, 1, 2, 1, 2);
        
    }

    private void SetTowSideAlignmentLayout()
    {
        ControlExtension.SetGridPosition(LeftItemBar, 0, 0, 2, 2);
        ControlExtension.SetGridPosition(PageContainer, 0, 2, 2, 2);
        ControlExtension.SetGridPosition(RightItemBar, 0, 4, 2, 1);
        ControlExtension.SetGridPosition(BottomItemBar, 2, 0, 1, 5);
        
        ControlExtension.SetGridPosition(LeftSplitter, 0, 1, 2, 1);
        ControlExtension.SetGridPosition(RightSplitter, 0, 3, 2, 1);
        ControlExtension.SetGridPosition(BottomSplitter, 1, 0, 1, 5);
    }
}