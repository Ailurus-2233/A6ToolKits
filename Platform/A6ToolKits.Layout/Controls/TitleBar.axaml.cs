using A6ToolKits.Commands;
using A6ToolKits.Common.Attributes.Layout;
using A6ToolKits.Layout.Controls.ControlCommand;
using A6ToolKits.Layout.Generator;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace A6ToolKits.Layout.Controls;

/// <summary>
///     Default 布局中的标题栏
/// </summary>
public partial class TitleBar : UserControl
{
    /// <summary>
    ///     构造函数
    /// </summary>
    public TitleBar()
    {
        InitializeComponent();
        Title.Text = WindowConfig.Instance.Title;
        TitleBarBorder.Background = new SolidColorBrush(WindowConfig.Instance.PrimaryColor);
        Height = WindowConfig.Instance.TitleBarHeight;
        TitleBarBorder.Padding = new Thickness(0 ,3);
        BuildWindowControlPanel();
    }
    
    private void BuildWindowControlPanel()
    {
        var config = WindowConfig.Instance;
    
        var minusAction = new MinimizeCommandHandler();
        var closeAction = new CloseCommandHandler();
        var maximizeAction = new MaximizeCommandHandler();
        var windowAction = new WindowCommandHandler();

        MinusButton = minusAction.GenerateButton<MinimizeCommandDefinition>(ButtonType.Icon);
        CloseButton = closeAction.GenerateButton<CloseCommandDefinition>(ButtonType.Icon);
        MaximizeButton = maximizeAction.GenerateButton<MaximizeCommandDefinition>(ButtonType.Icon);
        WindowButton = windowAction.GenerateButton<WindowCommandDefinition>(ButtonType.Icon);
    }
}