using A6ToolKits.Common.Action;
using A6ToolKits.Common.Action.CommonActions;
using A6ToolKits.Common.Attributes.Layout;
using A6ToolKits.Layout.Generator;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.VisualTree;
namespace A6ToolKits.Layout.Controls;

/// <summary>
///     标题栏
/// </summary>
public class HeaderBar : UserControl
{
    private bool _isDragging;
    private Point _startPosition;
    private Window? _window;
    
    /// <summary>
    ///     标题栏边框
    /// </summary>
    public Border? HeaderBarBorder { get; set; }
    /// <summary>
    ///    左侧控件
    /// </summary>
    public Control? LeftControl { get; set; }
    /// <summary>
    ///   右侧控件
    /// </summary>
    public Control? RightControl { get; set; }
    /// <summary>
    ///     中间控件
    /// </summary>
    public Control? CenterControl { get; set; }

    /// <summary>
    ///     标题栏构造函数
    /// </summary>
    public HeaderBar(Control leftControl, Control centerControl, Control? rightControl)
    {
        HeaderBarBorder = new Border
        {
            Background = Brushes.Transparent,
        };
        LeftControl = leftControl;
        CenterControl = centerControl;
        RightControl = rightControl ?? BuildWindowControlPanel();
        Content = HeaderBarBorder;
        var grid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitions("*,*,*")
        };
        HeaderBarBorder.Child = grid;
        if (LeftControl != null)
        {
            LeftControl.SetValue(Grid.ColumnProperty, 0);
            grid.Children.Add(LeftControl);
        }

        if (CenterControl != null)
        {
            CenterControl.SetValue(Grid.ColumnProperty, 1);
            grid.Children.Add(CenterControl);
        }

        if (RightControl != null)
        {
            RightControl.SetValue(Grid.ColumnProperty, 2);
            grid.Children.Add(RightControl);
        }
        
        HeaderBarBorder.PointerPressed += HeadBarPointerPressed;
        HeaderBarBorder.PointerMoved += HeadBarPointerMoved;
        HeaderBarBorder.PointerReleased += HeadBarPointerReleased;
    }


    private void HeadBarPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        _isDragging = true;
        _startPosition = e.GetPosition(this);
        if (_window == null) FindWindow();
    }

    private void HeadBarPointerMoved(object? sender, PointerEventArgs e)
    {
        if (!_isDragging) return;
        var currentPosition = e.GetPosition(this);
        var delta = currentPosition - _startPosition;

        if (_window == null) FindWindow();

        _window!.Position = new PixelPoint(
            _window.Position.X + (int)delta.X,
            _window.Position.Y + (int)delta.Y
        );
    }

    private void HeadBarPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        _isDragging = false;
    }

    private void FindWindow()
    {
        _window = this.GetVisualRoot() as Window ?? WindowGenerator.Window;
    }
    
    private StackPanel BuildWindowControlPanel()
    {
        var result = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center
        };

        if (_window == null) FindWindow();
        var menuHeight = WindowConfig.MenuHeight;

        var minusButton = new MinusAction().GenerateButton(ButtonType.Icon);
        var closeButton = new CloseAction().GenerateButton(ButtonType.Icon);
        var maximizeAction = new MaximizeAction(_window!);
        var maximizeButton = maximizeAction.GenerateButton(ButtonType.Icon);
        var windowAction = new WindowAction(_window!);
        var windowButton = windowAction.GenerateButton(ButtonType.Icon);

        minusButton.Height = menuHeight - 8;
        minusButton.Margin = new Thickness(4);
        closeButton.Height = menuHeight - 8;
        closeButton.Margin = new Thickness(4);
        maximizeButton.Height = menuHeight - 8;
        maximizeButton.Margin = new Thickness(4);
        windowButton.Height = menuHeight - 8;
        windowButton.Margin = new Thickness(4);

        result.Children.Add(minusButton);
        result.Children.Add(maximizeButton);
        result.Children.Add(windowButton);
        result.Children.Add(closeButton);

        maximizeButton.IsVisible = maximizeButton.IsEnabled;
        maximizeAction.CanRunChanged += (_, _) =>
        {
            maximizeButton.IsVisible = maximizeButton.IsEnabled;
            maximizeButton.Height = menuHeight - 8;
            maximizeButton.Margin = new Thickness(4);
        };

        windowButton.IsVisible = windowButton.IsEnabled;
        windowAction.CanRunChanged += (_, _) =>
        {
            windowButton.IsVisible = windowButton.IsEnabled;
            windowButton.Height = menuHeight - 8;
            windowButton.Margin = new Thickness(4);
        };

        return result;
    }
}