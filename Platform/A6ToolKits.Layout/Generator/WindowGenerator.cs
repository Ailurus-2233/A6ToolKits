using A6ToolKits.Helper.Config;
using A6ToolKits.Helper.Instance;
using A6ToolKits.Layout.ControlGenerator.Enums;
using A6ToolKits.Layout.Controls.Container;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
namespace A6ToolKits.Layout.Generator;

/// <summary>
///     窗口生成器
/// </summary>
internal static class WindowGenerator
{
    /// <summary>
    ///     实例创建器，用于初始化布局过程中控件实例的创建
    /// </summary>
    internal static IInstanceHelper? Creator { get; set; }

    internal static Window Window { get; private set; } = new();

    /// <summary>
    ///     生成一个窗口
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    internal static void GenerateWindow()
    {
        var layoutConfigItem = ConfigHelper.GetElements("Window")?.Item(0);
        if (layoutConfigItem == null)
            throw new Exception("Layout config not found.");
        
        var configItem = new LayoutConfigItem();
        configItem.GenerateFromXmlNode(layoutConfigItem);
        configItem.SetToResources();

        Window = new Window
        {
            Title = WindowConfig.Title,
            Height = WindowConfig.Height,
            Width = WindowConfig.Width,
            Background = Brushes.Transparent,
        };

        switch (WindowConfig.BorderStyle) {
            case WindowBorderType.Default:
                Window.ExtendClientAreaToDecorationsHint = false;
                Window.ExtendClientAreaTitleBarHeightHint = 0;
                break;
            case WindowBorderType.A6ToolKits:
            case WindowBorderType.None:
                Window.ExtendClientAreaToDecorationsHint = true;
                Window.ExtendClientAreaTitleBarHeightHint = -1;
                Window.SystemDecorations = SystemDecorations.None;
                break;
            default:
                throw new IndexOutOfRangeException("未知的 TopBarType 类型");
        }
        
        var grid = new Grid {
            RowDefinitions = new RowDefinitions("Auto,Auto,*,Auto,Auto")
        };
        var windowBorder = new Border
        {
            Background = Brushes.White,
            Child = grid,
            BorderBrush = new SolidColorBrush(WindowConfig.PrimaryColor),
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(5)
        };

        var topContainer = Creator?.GetOrCreateInstance<TopContainer>();
        var mainContainer = Creator?.GetOrCreateInstance<MainContainer>();
        var bottomContainer = Creator?.GetOrCreateInstance<BottomContainer>();
        
        topContainer?.GenerateTopContainer();
        topContainer?.SetValue(StyledElement.NameProperty, "TopContainer");
        mainContainer?.GenerateMainContainer();
        mainContainer?.SetValue(StyledElement.NameProperty, "MainContainer");
        bottomContainer?.GenerateBottomContainer();
        bottomContainer?.SetValue(StyledElement.NameProperty, "BottomContainer");
        
        Grid.SetRow(topContainer!, 0);
        Grid.SetRow(mainContainer!, 2);
        Grid.SetRow(bottomContainer!, 4);
        
        var splitLine1 = new Separator {
            Width = double.NaN,
            Height = 1,
            Margin = new Thickness(0)
        };
        
        var splitLine2 = new Separator {
            Width = double.NaN,
            Height = 1,
            Margin = new Thickness(0)
        };
        
        Grid.SetRow(splitLine1, 1);
        Grid.SetRow(splitLine2, 3);
        
        grid.Children.Add(topContainer!);
        grid.Children.Add(splitLine1);
        grid.Children.Add(mainContainer!);
        grid.Children.Add(splitLine2);
        grid.Children.Add(bottomContainer!);
        
        Window.Content = windowBorder;
    }
}