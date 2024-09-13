using Avalonia.Controls;

namespace A6ToolKits.Layout.Container.Controls;

/// <summary>
///     窗口容器，基于该窗口实现具体的布局设置
/// </summary>
public partial class WindowContainer : Window
{
    /// <summary>
    ///     构造函数
    /// </summary>
    public WindowContainer()
    {
        InitializeComponent();
    }

    /// <summary>
    ///     添加主面板
    /// </summary>
    /// <param name="mainPanel">
    ///     主面板对象
    /// </param>
    public void AddMainPanel(MainPanel mainPanel)
    {
        mainPanel.SetValue(Grid.RowProperty, 1);
        Target.Children.Add(mainPanel);
    }
}