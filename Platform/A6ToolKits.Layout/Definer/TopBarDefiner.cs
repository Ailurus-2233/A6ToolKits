using A6ToolKits.Common.Action;
using A6ToolKits.Common.Action.CommonActions;
using A6ToolKits.Common.Attributes.Layout;
using A6ToolKits.Layout.Definer.Interfaces;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
namespace A6ToolKits.Layout.Definer;

public abstract class TopBarDefiner : IDefiner<Grid> {

    /// <summary>
    ///     生成 TopBar 所属的 Window
    /// </summary>
    public required Window Window { get; set; }

    /// <summary>
    ///    如果TopBar 类型是 Defined 是 Header 的高度，如果TopBar 类型是 Default 则是菜单栏的高度
    /// </summary>
    public abstract double HeaderHeight { get; set; }
    /// <summary>
    ///     工具栏的高度
    /// </summary>
    public abstract double ToolBarHeight { get; set; }

    /// <summary>
    ///     TopBar 的类型
    /// </summary>
    public abstract TopBarType TopBarType { get; set; }

    /// <summary>
    ///     菜单定义器，用于生成菜单控件
    /// </summary>
    public abstract MenuDefiner MenuDefiner { get; set; }

    /// <summary>
    ///     工具栏定义器，用于生成工具栏控件
    /// </summary>
    public abstract ToolBarDefiner ToolBarDefiner { get; set; }

    /// <summary>
    ///     生成一个TopBar
    /// </summary>
    /// <returns></returns>
    public Grid Build() {
        var result = new Grid() {
            RowDefinitions = new RowDefinitions($"{HeaderHeight},Auto,{ToolBarHeight}")
        };

        if (TopBarType == TopBarType.Defined) {
            var headerBar = BuildDefinedHeaderBar();
            Grid.SetRow(headerBar, 0);
            result.Children.Add(headerBar);
        }

        if (TopBarType == TopBarType.Default) {
            var menu = BuildMenuPanel();
            Grid.SetRow(menu, 0);
            result.Children.Add(menu);
        }

        if (TopBarType == TopBarType.Custom) {
            // TODO: 自定义菜单逻辑未确定   
        }

        var splitLine = new Separator {
            Width = double.NaN,
            Height = 1,
            Margin = new Thickness(0)
        };
        Grid.SetRow(splitLine, 1);
        result.Children.Add(splitLine);

        var toolBar = ToolBarDefiner.Build();
        Grid.SetRow(toolBar, 2);
        result.Children.Add(toolBar);
        return result;
    }

    private Grid BuildDefinedHeaderBar() {
        var result = new Grid {
            ColumnDefinitions = new ColumnDefinitions("*,*,*")
        };

        var left = new StackPanel() {
            Orientation = Orientation.Horizontal
        };
        left.Children.Add(BuildMenuPanel());
        Grid.SetColumn(left, 0);
        result.Children.Add(left);

        var right = BuildWindowControlPanel();
        Grid.SetColumn(right, 2);
        result.Children.Add(right);

        var title = new TextBlock {
            Text = Window.Title,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            IsEnabled = false
        };
        title.SetValue(Visual.ZIndexProperty, -1);
        Grid.SetColumn(title, 1);
        result.Children.Add(title);

        return result;
    }

    private StackPanel BuildWindowControlPanel() {
        var result = new StackPanel {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center
        };

        var minusButton = new MinusAction().GenerateButton(ButtonType.Icon);
        var closeButton = new CloseAction().GenerateButton(ButtonType.Icon);
        var maximizeAction = new MaximizeAction(Window);
        var maximizeButton = maximizeAction.GenerateButton(ButtonType.Icon);
        var windowAction = new WindowAction(Window);
        var windowButton = windowAction.GenerateButton(ButtonType.Icon);

        minusButton.Height = HeaderHeight - 8;
        minusButton.Margin = new Thickness(4);
        closeButton.Height = HeaderHeight - 8;
        closeButton.Margin = new Thickness(4);
        maximizeButton.Height = HeaderHeight - 8;
        maximizeButton.Margin = new Thickness(4);
        windowButton.Height = HeaderHeight - 8;
        windowButton.Margin = new Thickness(4);

        result.Children.Add(minusButton);
        result.Children.Add(maximizeButton);
        result.Children.Add(windowButton);
        result.Children.Add(closeButton);

        maximizeButton.IsVisible = maximizeButton.IsEnabled;
        maximizeAction.CanRunChanged += (_, _) => {
            maximizeButton.IsVisible = maximizeButton.IsEnabled;
            maximizeButton.Height = HeaderHeight - 8;
            maximizeButton.Margin = new Thickness(4);
        };

        windowButton.IsVisible = windowButton.IsEnabled;
        windowAction.CanRunChanged += (_, _) => {
            windowButton.IsVisible = windowButton.IsEnabled;
            windowButton.Height = HeaderHeight - 8;
            windowButton.Margin = new Thickness(4);
        };

        return result;
    }

    private Menu BuildMenuPanel() {
        Menu result = new();
        if (TopBarType == TopBarType.Defined) {
            MenuDefiner.MenuType = MenuType.Icon;
            result = MenuDefiner.Build();
            result.Height = HeaderHeight - 2;
            result.Width = HeaderHeight - 2;
            result.Padding = new Thickness(5);
        }

        if (TopBarType == TopBarType.Default) {
            MenuDefiner.MenuType = MenuType.Row;
            result = MenuDefiner.Build();
        }

        if (TopBarType == TopBarType.Custom) {
            // TODO: 自定义菜单逻辑未确定   
        }
        return result;
    }
}
/// <summary>
///     生成窗口的 HeaderBar 类型
/// </summary>
public enum TopBarType {
    /// <summary>
    ///     原生窗口的类型
    /// </summary>
    Default,
    /// <summary>
    ///     Layout 模块定义的 HeaderBar 布局类型
    /// </summary>
    Defined,
    /// <summary>
    ///     自定义 HeaderBar 布局类型
    /// </summary>
    Custom,
}