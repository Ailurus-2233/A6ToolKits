using A6ToolKits.Action;
using A6ToolKits.Common.Attributes.Layout;
using A6ToolKits.Layout.Controls.WindowActions;
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
    
        var minusButton = new MinimizeAction().GenerateButton(ButtonType.Icon);
        var closeButton = new CloseAction().GenerateButton(ButtonType.Icon);
        var maximizeAction = new MaximizeAction();
        var maximizeButton = maximizeAction.GenerateButton(ButtonType.Icon);
        var windowAction = new WindowingAction();
        var windowButton = windowAction.GenerateButton(ButtonType.Icon);
        
        ControlBar.Children.Add(minusButton);
        ControlBar.Children.Add(maximizeButton);
        ControlBar.Children.Add(windowButton);
        ControlBar.Children.Add(closeButton);
    
        SetButtonStyle(minusButton);
        SetButtonStyle(closeButton);
        SetButtonStyle(maximizeButton);
        SetButtonStyle(windowButton);
        
        maximizeButton.IsVisible = maximizeButton.IsEnabled;
        maximizeAction.CanRunChanged += (_, _) =>
        {
            maximizeButton.IsVisible = maximizeButton.IsEnabled;
            SetButtonStyle(maximizeButton);
        };
    
        windowButton.IsVisible = windowButton.IsEnabled;
        windowAction.CanRunChanged += (_, _) =>
        {
            windowButton.IsVisible = windowButton.IsEnabled;
            SetButtonStyle(windowButton);
        };
    }

    private void SetButtonStyle(Button button)
    {
        button.Background = new SolidColorBrush(Colors.Transparent);
        button.BorderBrush = new SolidColorBrush(Colors.Transparent);
    }
}