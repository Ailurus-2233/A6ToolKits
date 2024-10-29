using A6ToolKits.Bootstrapper;
using A6ToolKits.Common.Action;
using A6ToolKits.Common.Action.CommonActions;
using A6ToolKits.Common.Attributes.Layout;
using A6ToolKits.Layout.Generator;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;

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
    }
    
    private void BuildWindowControlPanel()
    {
        var config = WindowConfig.Instance;
        var height = config.TitleBarHeight;

        var minusButton = new MinimizeAction().GenerateButton(ButtonType.Icon);
        var closeButton = new CloseAction().GenerateButton(ButtonType.Icon);
        var maximizeAction = new MaximizeAction();
        var maximizeButton = maximizeAction.GenerateButton(ButtonType.Icon);
        var windowAction = new WindowingAction();
        var windowButton = windowAction.GenerateButton(ButtonType.Icon);

        minusButton.Height = height - 8;
        minusButton.Margin = new Thickness(4);
        closeButton.Height = height - 8;
        closeButton.Margin = new Thickness(4);
        maximizeButton.Height = height - 8;
        maximizeButton.Margin = new Thickness(4);
        windowButton.Height = height - 8;
        windowButton.Margin = new Thickness(4);

        ControlBar.Children.Add(minusButton);
        ControlBar.Children.Add(maximizeButton);
        ControlBar.Children.Add(windowButton);
        ControlBar.Children.Add(closeButton);

        maximizeButton.IsVisible = maximizeButton.IsEnabled;
        maximizeAction.CanRunChanged += (_, _) =>
        {
            maximizeButton.IsVisible = maximizeButton.IsEnabled;
            maximizeButton.Height = height - 8;
            maximizeButton.Margin = new Thickness(4);
        };

        windowButton.IsVisible = windowButton.IsEnabled;
        windowAction.CanRunChanged += (_, _) =>
        {
            windowButton.IsVisible = windowButton.IsEnabled;
            windowButton.Height = height - 8;
            windowButton.Margin = new Thickness(4);
        };
    }
}