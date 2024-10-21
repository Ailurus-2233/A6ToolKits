using A6ToolKits.Common.Action;
using A6ToolKits.Common.Action.CommonActions;
using A6ToolKits.Common.Attributes.Layout;
using A6ToolKits.Common.Attributes.MVVM;
using A6ToolKits.Layout.ControlGenerator.Enums;
using A6ToolKits.Layout.Generator;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
namespace A6ToolKits.Layout.Controls.Container;

/// <summary>
///     顶部容器,用于放置标题栏，菜单栏和工具栏
/// </summary>
[AutoRegister(typeof(TopContainer), RegisterType.Singleton)]
public class TopContainer : UserControl
{
    private double MenuHeight => WindowConfig.MenuHeight;
    private double TopBarHeight => WindowConfig.TopBarHeight;

    private Window Window => WindowGenerator.Window;

    private MenuGenerator? MenuGenerator { get; set; }
    private ToolBarGenerator? ToolBarGenerator { get; set; }
    
    /// <summary>
    ///     顶部容器构造函数
    /// </summary>
    public TopContainer()
    {
        MenuGenerator = WindowGenerator.Creator?.GetOrCreateInstance<MenuGenerator>();
        ToolBarGenerator = WindowGenerator.Creator?.GetOrCreateInstance<ToolBarGenerator>();
    }

    /// <summary>
    ///     生成顶部容器
    /// </summary>
    public void GenerateTopContainer()
    {
        var topControl = GenerateTopControl();
        var toolBar = ToolBarGenerator?.Generate();
        
        // 如果两个控件都为空，则不生成
        if (topControl == null && toolBar == null) return;
        
        // 如果其中一个控件为空，则直接返回另一个控件
        if (topControl != null && toolBar == null)
        {
            Content = topControl;
            return;
        }
        if (topControl == null && toolBar != null)
        {
            toolBar.Name = "ToolBar";
            Content = toolBar;
            return;
        }
        
        // 生成基本的顶部容器
        var grid = new Grid
        {
            RowDefinitions = new RowDefinitions($"{MenuHeight},Auto,{TopBarHeight}")
        };
        Grid.SetRow(topControl!, 0);
        grid.Children.Add(topControl!);
        var splitLine = new Separator
        {
            Height = 1,
            Width = double.NaN,
            Margin = new Thickness(0)
        };
        Grid.SetRow(splitLine, 1);
        grid.Children.Add(splitLine);
        Grid.SetRow(toolBar!, 2);
        grid.Children.Add(toolBar!);
        Content = grid;
    }

    /// <summary>
    ///     生成顶部控件
    /// </summary>
    /// <returns>
    ///     生成的控件:
    ///         1. 默认边框类型下返回菜单控件
    ///         2. A6ToolKits边框类型下返回一个Grid控件，包含菜单控件，标题栏和窗口控制按钮
    ///         3. 无边框类型下返回一个空Grid控件
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     未知的边框类型
    /// </exception>
    private Control? GenerateTopControl()
    {
        var menu = MenuGenerator?.Generate();
        switch (WindowConfig.BorderStyle)
        {
            case WindowBorderType.Default: {
                if (menu == null) return null;
                var result = new Grid();
                Grid.SetRow(menu, 0);
                result.Children.Add(menu);
                return result;
            }
            case WindowBorderType.A6ToolKits: {
                var left = new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                    Height = TopBarHeight
                };
                if (menu != null) left.Children.Add(menu);
                var title = new TextBlock
                {
                    Text = Window.Title,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    IsEnabled = false
                };

                var result = new HeaderBar(left, title, null)
                {
                    Height = TopBarHeight,
                    Name = "HeaderBar"
                };
                return result;
            }
            case WindowBorderType.None:
                return new Grid
                {
                    Name = "HeaderBar"
                };
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}