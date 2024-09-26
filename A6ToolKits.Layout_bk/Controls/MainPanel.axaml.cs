using A6ToolKits.Layout.Enums;
using A6ToolKits.UIPackage.Controls.TabControl.Models;
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

    public void SetLayout(LayoutAlignment alignment, double leftWidth = 0, double rightWidth = 0, double bottomHeight = 0)
    {
        var leftStr = leftWidth > 0 ? $"{leftWidth}" : "Auto";
        var rightStr = rightWidth > 0 ? $"{rightWidth}" : "Auto";
        var bottomStr = bottomHeight > 0 ? $"{bottomHeight}" : "Auto";
        
        MainGrid.ColumnDefinitions = new ColumnDefinitions($"{leftStr},Auto,*,Auto,{rightStr}");
        MainGrid.RowDefinitions = new RowDefinitions($"*,Auto,{bottomStr}");

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
        PrimarySideBar.SetGridPosition(0, 0, 2, 2);
        LeftSplitter.SetGridPosition(0, 1, 2, 1);
        PageContainer.SetGridPosition(0, 2, 2, 2);
        RightSplitter.SetGridPosition(0, 3, 3, 1);
        SecondarySideBar.SetGridPosition(0, 4, 3, 1);
        BottomSplitter.SetGridPosition(1, 0, 1, 4);
        PanelBar.SetGridPosition(2, 0, 1, 4);
    }

    private void SetRightAlignmentLayout()
    {
        PrimarySideBar.SetGridPosition(0, 0, 3, 2);
        PageContainer.SetGridPosition(0, 2, 2, 2);
        SecondarySideBar.SetGridPosition(0, 4, 2);
        PanelBar.SetGridPosition(2, 2, 1, 4);
        
        LeftSplitter.SetGridPosition(0, 1, 3);
        RightSplitter.SetGridPosition(0, 3, 2);
        BottomSplitter.SetGridPosition(1, 2, 1, 4);
    }

    private void SetCenterAlignmentLayout()
    {
        PrimarySideBar.SetGridPosition(0, 0, 3, 2);
        PageContainer.SetGridPosition(0, 2, 2, 2);
        SecondarySideBar.SetGridPosition(0, 4, 3);
        PanelBar.SetGridPosition(2, 2, 1, 2);
        
        LeftSplitter.SetGridPosition(0, 1, 3);
        RightSplitter.SetGridPosition(0, 3, 3);
        BottomSplitter.SetGridPosition(1, 2, 1, 2);
        
    }

    private void SetTowSideAlignmentLayout()
    {
        PrimarySideBar.SetGridPosition(0, 0, 2, 2);
        PageContainer.SetGridPosition(0, 2, 2, 2);
        SecondarySideBar.SetGridPosition(0, 4, 2);
        PanelBar.SetGridPosition(2, 0, 1, 5);
        
        LeftSplitter.SetGridPosition(0, 1, 2);
        RightSplitter.SetGridPosition(0, 3, 2);
        BottomSplitter.SetGridPosition(1, 0, 1, 5);
    }

    public void SetTabCollection(TabCollection primaryTabs, TabCollection secondaryTabs, TabCollection bottomTabs)
    {
        ActivityBar.PrimaryTabHeader.TabCollection = primaryTabs;
        PrimarySideBar.PrimaryTabBody.TabCollection = primaryTabs;

        SecondarySideBar.SecondaryTabHeader.TabCollection = secondaryTabs;
        SecondarySideBar.SecondaryTabBody.TabCollection = secondaryTabs;

        PanelBar.PanelTabHeader.TabCollection = bottomTabs;
        PanelBar.PanelTabBody.TabCollection = bottomTabs;
    }
}