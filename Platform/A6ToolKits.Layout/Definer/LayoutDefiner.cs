using A6ToolKits.Common.Action;
using A6ToolKits.Common.Action.CommonActions;
using A6ToolKits.Common.Attributes.Layout;
using A6ToolKits.Layout.Definer.Interfaces;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Platform;

namespace A6ToolKits.Layout.Definer;

public abstract class LayoutDefiner : IDefiner<Window> {
    private readonly Window _window = new();
    public abstract TopBarDefiner TopBarDefiner { get; set; }
    public abstract StatusBarDefiner StatusBarDefiner { get; set; }

    public abstract PageDefiner PageDefiner { get; set; }

    public Control TopContainer { get; set; }

    public Control BottomContainer { get; set; }

    public Control CenterContainer { get; set; }

    public abstract double StatusBarHeight { get; set; }

    public abstract string Title { get; set; }

    public abstract Color PrimaryColor { get; set; }

    public abstract double WindowHeight { get; set; }
    public abstract double WindowWidth { get; set; }

    public abstract TopBarType TopBarType { get; set; }

    /// <summary>
    ///     根据定义器生成自定义窗口
    /// </summary>
    /// <returns>
    ///     最终展示的窗口
    /// </returns>
    /// <exception cref="IndexOutOfRangeException">
    ///    未知的 HeaderBarType 类型
    /// </exception>
    public Window Build() {
        switch (TopBarType) {
            case TopBarType.Default:
                _window.ExtendClientAreaToDecorationsHint = false;
                _window.ExtendClientAreaTitleBarHeightHint = 0;
                break;
            case TopBarType.Defined:
            case TopBarType.Custom:
                _window.ExtendClientAreaToDecorationsHint = true;
                _window.ExtendClientAreaTitleBarHeightHint = -1;
                break;
            default:
                throw new IndexOutOfRangeException("未知的 TopBarType 类型");
        }
        _window.ExtendClientAreaChromeHints = ExtendClientAreaChromeHints.OSXThickTitleBar;

        _window.Height = WindowHeight;
        _window.Width = WindowWidth;

        var grid = new Grid {
            Name = DefinerResources.MainGridName,
            RowDefinitions = new RowDefinitions("Auto,Auto,*,Auto,Auto")
        };
        
        var topContainer = TopBarDefiner.Build();
        Grid.SetRow(topContainer, 0);

        var splitLine1 = new Separator {
            Width = double.NaN,
            Height = 1,
            Margin = new Thickness(0)
        };
        Grid.SetRow(splitLine1, 1);

        var page = PageDefiner.Build();
        Grid.SetRow(page, 2);

        var splitLine2 = new Separator {
            Width = double.NaN,
            Height = 1,
            Margin = new Thickness(0)
        };
        Grid.SetRow(splitLine2, 3);

        var statusBar = StatusBarDefiner.Build();
        Grid.SetRow(statusBar, 4);

        grid.Children.Add(topContainer);
        grid.Children.Add(splitLine1);
        grid.Children.Add(page);
        grid.Children.Add(splitLine2);
        grid.Children.Add(statusBar);

        _window.Content = grid;

        _window.BorderThickness = new Thickness(1);
        _window.CornerRadius = new CornerRadius(5);
        return _window;
    }
}