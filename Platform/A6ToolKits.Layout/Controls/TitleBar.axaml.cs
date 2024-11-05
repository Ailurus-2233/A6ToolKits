using A6ToolKits.Attributes.Layout;
using A6ToolKits.Bootstrapper.Interfaces;
using A6ToolKits.Commands;
using A6ToolKits.Events;
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
        TitleBarBorder.Padding = new Thickness(0, 3);
        BuildWindowControlPanel();
    }

    private void BuildWindowControlPanel()
    {
        var config = WindowConfig.Instance;
        var height = config.TitleBarHeight;

        var minusAction = new MinimizeCommand();
        var closeAction = new CloseCommand();
        var maximizeAction = new MaximizeCommand();
        var windowAction = new WindowCommand();

        MinusButton.Content = minusAction.GenerateButton(ButtonType.Icon, height);
        CloseButton.Content = closeAction.GenerateButton(ButtonType.Icon, height);
        MaximizeButton.Content = maximizeAction.GenerateButton(ButtonType.Icon, height);
        WindowButton.Content = windowAction.GenerateButton(ButtonType.Icon, height);

        IoC.GetInstance<IEventAggregator>()?.Subscribe<BootFinishedEvent>(_ =>
        {
            var window = IoC.GetInstance<IApplicationController>()?.GetMainWindow();
            window?.GetObservable(Window.WindowStateProperty).Subscribe(state =>
            {
                switch (state)
                {
                    case WindowState.Maximized:
                        MaximizeButton.IsVisible = false;
                        WindowButton.IsVisible = true;
                        break;
                    case WindowState.Normal:
                        MaximizeButton.IsVisible = true;
                        WindowButton.IsVisible = false;
                        break;
                    case WindowState.Minimized:
                    case WindowState.FullScreen:
                    default:
                        break;
                }
            });
            if (window?.WindowState == WindowState.Maximized)
            {
                MaximizeButton.IsVisible = false;
                WindowButton.IsVisible = true;
            }

            if (window?.WindowState == WindowState.Normal)
            {
                MaximizeButton.IsVisible = true;
                WindowButton.IsVisible = false;
            }
        });
    }
}