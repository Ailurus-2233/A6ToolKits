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
        WindowBorder.Background = new SolidColorBrush(WindowConfig.Instance.BackgroundColor);
    }
}