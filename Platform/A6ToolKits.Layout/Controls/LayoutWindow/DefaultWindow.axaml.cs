using A6ToolKits.Layout.Generator;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;

namespace A6ToolKits.Layout.Controls.LayoutWindow;

/// <summary>
///     默认窗口结构
/// </summary>
public partial class DefaultWindow : WindowBase
{
    /// <summary>
    ///     构造函数
    /// </summary>
    public DefaultWindow()
    {
        InitializeComponent();
        
        WindowBorder.Background = new SolidColorBrush(WindowConfig.Instance.BackgroundColor);
        WindowBorder.BorderBrush = new SolidColorBrush(WindowConfig.Instance.PrimaryColor);

        TitleBar.PointerPressed += TitleBar_PointerPressed;
    }
    
    private void TitleBar_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            BeginMoveDrag(e);
        }
    }
}