using A6ToolKits.ApplicationController;
using A6ToolKits.Container;
using A6ToolKits.EventAggregator;
using A6ToolKits.Events;
using A6ToolKits.Layout.Attributes;
using A6ToolKits.Layout.Controls.ControlCommand;
using A6ToolKits.Layout.Exceptions;
using A6ToolKits.Layout.Generator;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

// ReSharper disable InvertIf
// ReSharper disable ConvertIfStatementToSwitchStatement

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
            var window = IoC.GetInstance<IApplicationController>()?.MainWindow;
            if (window == null) 
                throw new WindowUninitializedException();
            var observable = window.GetObservable(Window.WindowStateProperty);
            observable.Subscribe(new WindowStateObserver(this));
            UpdateControlButton(window.WindowState);
        });
    }

    /// <summary>
    ///     更新控制按钮
    /// </summary>
    /// <param name="state">
    ///     如果是最大化或全屏状态，则隐藏最大化按钮，显示还原按钮
    ///     如果是正常状态，则显示最大化按钮，隐藏还原按钮
    /// </param>
    private void UpdateControlButton(WindowState state)
    {
        switch (state)
        {
            case WindowState.Maximized:
            case WindowState.FullScreen:
                MaximizeButton.IsVisible = false;
                WindowButton.IsVisible = true;
                break;
            case WindowState.Normal:
                MaximizeButton.IsVisible = true;
                WindowButton.IsVisible = false;
                break;
            case WindowState.Minimized:
            default:
                break;
        }
    }
    
    private class WindowStateObserver(TitleBar titleBar) : IObserver<WindowState>
    {
        public void OnCompleted()
        { }

        public void OnError(Exception error)
        { }

        public void OnNext(WindowState value)
        {
            titleBar.UpdateControlButton(value);
        }
    }
}