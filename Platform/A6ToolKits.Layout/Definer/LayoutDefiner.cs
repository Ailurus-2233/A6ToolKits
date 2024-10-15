using System.Drawing;
using A6ToolKits.Common.Action;
using A6ToolKits.Common.Action.CommonActions;
using A6ToolKits.Common.Attributes.Layout;
using A6ToolKits.Layout.Definer.Interfaces;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Platform;
using Avalonia.Styling;
using DynamicData;

namespace A6ToolKits.Layout.Definer;

public abstract class LayoutDefiner : IDefiner<Window>
{
    private readonly Window _window = new();

    public abstract MenuDefiner MenuDefiner { get; set; }

    public abstract ToolBarDefiner ToolBarDefiner { get; set; }

    public abstract StatusBarDefiner StatusBarDefiner { get; set; }

    public abstract PageDefiner PageDefiner { get; set; }

    public abstract double HeaderHeight { get; set; }

    public abstract double ToolBarHeight { get; set; }

    public abstract double StatusBarHeight { get; set; }

    public abstract string Title { get; set; }
    
    public abstract Color PrimaryColor { get; set; }
    
    public abstract double WindowHeight { get; set; }
    public abstract double WindowWidth { get; set; }

    public Window Build()
    {
        _window.ExtendClientAreaToDecorationsHint = true;
        _window.ExtendClientAreaChromeHints = ExtendClientAreaChromeHints.OSXThickTitleBar;
        _window.ExtendClientAreaTitleBarHeightHint = -1;

        _window.PropertyChanged += (sender, args) =>
        {
            if (args.Property.Name != "WindowState") return;
            _window.Padding = _window.WindowState == WindowState.Maximized ? new Thickness(10) : new Thickness(0);
        };

        _window.Height = WindowHeight;
        _window.Width = WindowWidth;

        var grid = new Grid
        {
            Name = "MainGrid",
            RowDefinitions = new RowDefinitions($"Auto,Auto,*,Auto,{StatusBarHeight}")
        };

        var topContainer = BuildTopContainer();
        Grid.SetRow(topContainer, 0);

        var splitLine1 = new Separator
        {
            Width = double.NaN,
            Height = 1,
            Margin = new Thickness(0)
        };
        Grid.SetRow(splitLine1, 1);

        var page = PageDefiner.Build();
        Grid.SetRow(page, 2);

        var splitLine2 = new Separator
        {
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

        return _window;
    }

    private Grid BuildHeaderBar()
    {
        var result = new Grid
        {
            ColumnDefinitions = new ColumnDefinitions("*,*,*")
        };

        var left = new StackPanel()
        {
            Orientation = Orientation.Horizontal
        };
        var menu = MenuDefiner.Build();
        menu.Height = HeaderHeight - 2;
        menu.Width = HeaderHeight - 2;
        menu.Padding = new Thickness(5);
        left.Children.Add(menu);
        Grid.SetColumn(left, 0);
        result.Children.Add(left);

        var right = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Center
        };

        var minusButton = new MinusAction().GenerateButton(ButtonType.Icon);
        var closeButton = new CloseAction().GenerateButton(ButtonType.Icon);
        var maximizeAction = new MaximizeAction(_window);
        var maximizeButton = maximizeAction.GenerateButton(ButtonType.Icon);
        var windowAction = new WindowAction(_window);
        var windowButton = windowAction.GenerateButton(ButtonType.Icon);

        minusButton.Height = HeaderHeight - 8;
        minusButton.Margin = new Thickness(4);
        closeButton.Height = HeaderHeight - 8;
        closeButton.Margin = new Thickness(4);
        maximizeButton.Height = HeaderHeight - 8;
        maximizeButton.Margin = new Thickness(4);
        windowButton.Height = HeaderHeight - 8;
        windowButton.Margin = new Thickness(4);

        right.Children.Add(minusButton);
        right.Children.Add(maximizeButton);
        right.Children.Add(windowButton);
        right.Children.Add(closeButton);

        Grid.SetColumn(right, 2);
        result.Children.Add(right);

        maximizeButton.IsVisible = maximizeButton.IsEnabled;
        maximizeAction.CanRunChanged += (_, _) =>
        {
            maximizeButton.IsVisible = maximizeButton.IsEnabled;
            maximizeButton.Height = HeaderHeight - 8;
            maximizeButton.Margin = new Thickness(4);
        };

        windowButton.IsVisible = windowButton.IsEnabled;
        windowAction.CanRunChanged += (_, _) =>
        {
            windowButton.IsVisible = windowButton.IsEnabled;
            windowButton.Height = HeaderHeight - 8;
            windowButton.Margin = new Thickness(4);
        };

        var title = new TextBlock
        {
            Text = Title,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            IsEnabled = false
        };
        title.SetValue(Visual.ZIndexProperty, -1);
        Grid.SetColumn(title, 1);
        result.Children.Add(title);

        return result;
    }

    private Grid BuildTopContainer()
    {
        var result = new Grid
        {
            RowDefinitions = new RowDefinitions($"{HeaderHeight},Auto,{ToolBarHeight}")
        };

        var headerBar = BuildHeaderBar();
        Grid.SetRow(headerBar, 0);
        result.Children.Add(headerBar);

        var splitLine = new Separator
        {
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
}