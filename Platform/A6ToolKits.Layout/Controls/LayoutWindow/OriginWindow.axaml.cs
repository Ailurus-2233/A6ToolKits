using A6ToolKits.Layout.Generator;
using Avalonia.Controls;
using Avalonia.Media;
namespace A6ToolKits.Layout.Controls.LayoutWindow;

/// <summary>
///     默认布局的窗口
/// </summary>
public partial class OriginWindow : WindowBase
{
    /// <summary>
    ///     默认窗口构造函数
    /// </summary>
    public OriginWindow()
    {
        InitializeComponent();
        if (MenuBar.Menu.Height == 0)
            MenuBarSeparator.Height = 0;
        
        if (ToolBar.ToolBarPanel.Height == 0)
            ToolBarSeparator.Height = 0;
        
        if (StatusBar.StatusBarPanel.Height == 0)
            StatusBarSeparator.Height = 0;
        
        InitBorder();
    }

    private void InitBorder()
    {
        MainBorder.Background = new SolidColorBrush(WindowConfig.Instance.BackgroundColor);
    }
}