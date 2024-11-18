using A6ToolKits.Command;
using A6ToolKits.Layout.Attributes;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;

namespace A6ToolKits.Layout.Generator;

public static class CommandControlGenerator
{
    /// <summary>
    ///     生成一个可以运行命令的控件
    /// </summary>
    /// <param name="commandControlType">
    ///     控件类型
    /// </param>
    /// <param name="height">
    ///     控件高度
    /// </param>
    /// <returns>
    ///     返回的控件
    /// </returns>
    public static Control CreateControl(this CommandBase command, CommandControlType commandControlType, double height)
    {
        return commandControlType switch
        {
            CommandControlType.TextMenuItem => command.GenerateMenuItem(MenuItemType.Text, height),
            CommandControlType.IconAndTextMenuItem => command.GenerateMenuItem(MenuItemType.IconAndText, height),
            CommandControlType.IconButton => command.GenerateButton(ButtonType.Icon, height),
            CommandControlType.TextButton => command.GenerateButton(ButtonType.Text, height),
            CommandControlType.IconAndTextButton => command.GenerateButton(ButtonType.IconAndText, height),
            _ => new Control()
        };
    }

    #region MenuItem

    /// <summary>
    ///     基于 ActionBase 生成一个菜单项，用于显示在菜单中
    /// </summary>
    /// <param name="command">
    ///     Command 对象
    /// </param>
    /// <param name="type">
    ///     菜单类型
    /// </param>
    /// <param name="height">
    ///     控件高度
    /// </param>
    /// <returns>
    ///     生成的菜单项
    /// </returns>
    public static MenuItem GenerateMenuItem(this CommandBase command, MenuItemType type, double height)
    {
        var nameBinding = new Binding(nameof(command.Name))
        {
            Source = command
        };
        var iconBinding = new Binding(nameof(command.Image))
        {
            Source = command
        };
        var enableBinding = new Binding(nameof(command.Enable))
        {
            Source = command
        };
        var toolTipBinding = new Binding(nameof(command.ToolTip))
        {
            Source = command
        };
        var visibilityBinding = new Binding(nameof(command.Visible))
        {
            Source = command
        };

        var menuItem = new MenuItem();

        menuItem.Bind(InputElement.IsEnabledProperty, enableBinding);
        menuItem.Bind(Visual.IsVisibleProperty, visibilityBinding);

        var grid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitions("Auto,*"),
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Center
        };

        var image = new Image();
        image.Bind(Image.SourceProperty, iconBinding);
        image.Margin = new Thickness(height * 0.1);
        Grid.SetColumn(image, 0);
        var textBlock = new TextBlock();
        textBlock.Bind(TextBlock.TextProperty, nameBinding);
        textBlock.Margin = new Thickness(10, 0, 0, 0);
        Grid.SetColumn(textBlock, 1);

        switch (type)
        {
            case MenuItemType.Text:
                break;
            case MenuItemType.IconAndText:
                grid.Children.Add(image);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        grid.Children.Add(textBlock);
        menuItem.Header = grid;
        menuItem.Height = height;

        var toolTip = new ToolTip();
        toolTip.Bind(ContentControl.ContentProperty, toolTipBinding);
        ToolTip.SetTip(menuItem, toolTip);

        menuItem.Click += (_, _) =>
        {
            command.Run().Wait();
            command.Update();
        };
        return menuItem;
    }

    #endregion

    #region Button

    /// <summary>
    ///     基于 ActionBase 生成一个按钮，用于显示在工具栏中
    /// </summary>
    /// <param name="command">
    ///     CommandBase 对象
    /// </param>
    /// <param name="type">
    ///     按钮类型
    /// </param>
    /// <param name="height">
    ///     控件高度
    /// </param>
    /// <returns>
    ///     生成的按钮
    /// </returns>
    public static Button GenerateButton(this CommandBase command, ButtonType type, double height)
    {
        var enableBinding = new Binding(nameof(command.Enable))
        {
            Source = command
        };
        var visibilityBinding = new Binding(nameof(command.Visible))
        {
            Source = command
        };
        var toolTipBinding = new Binding(nameof(command.ToolTip))
        {
            Source = command
        };

        var button = new Button();
        button.Click += (_, _) => command.Run();

        button.Bind(InputElement.IsEnabledProperty, enableBinding);
        button.Bind(Visual.IsVisibleProperty, visibilityBinding);

        var toolTip = new ToolTip();
        toolTip.Bind(ContentControl.ContentProperty, toolTipBinding);
        ToolTip.SetTip(button, toolTip);

        button.SetTemplate(command, type);
        button.Height = height;
        button.Background = Brushes.Transparent;

        return button;
    }

    private static void SetTemplate(this Button button, CommandBase command, ButtonType type)
    {
        var nameBinding = new Binding(nameof(command.Name))
        {
            Source = command
        };
        var iconBinding = new Binding(nameof(command.Image))
        {
            Source = command
        };

        button.VerticalAlignment = VerticalAlignment.Center;
        button.HorizontalAlignment = HorizontalAlignment.Center;
        button.HorizontalContentAlignment = HorizontalAlignment.Stretch;
        button.VerticalContentAlignment = VerticalAlignment.Stretch;
        button.Padding = new Thickness(0);

        var image = new Image
        {
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center
        };
        image.Bind(Image.SourceProperty, iconBinding);

        var textBlock = new TextBlock
        {
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center
        };
        textBlock.Bind(TextBlock.TextProperty, nameBinding);

        switch (type)
        {
            case ButtonType.Icon:
                button.Content = image;
                button.PropertyChanged += (_, args) =>
                {
                    if (args.Property.Name is "Bounds")
                        button.SetButtonSize();
                };
                break;
            case ButtonType.IconAndText:
                var stackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };
                stackPanel.Children.Add(image);
                stackPanel.Children.Add(textBlock);
                button.Content = stackPanel;
                button.PropertyChanged += (_, args) =>
                {
                    if (args.Property.Name is "Bounds")
                        button.SetButtonSize();
                };
                break;
            case ButtonType.Text:
            default:
                button.Content = textBlock;
                break;
        }
    }

    private static void SetButtonSize(this Button button)
    {
        var bounds = button.Bounds;
        button.Width = bounds.Height;
        button.Height = bounds.Height;
        button.Padding = new Thickness(button.Width * 0.19);
    }

    #endregion
}

/// <summary>
///     可以执行命令的控件类型
/// </summary>
public enum CommandControlType
{
    /// <summary>
    ///     图标按钮
    /// </summary>
    IconButton,

    /// <summary>
    ///     文本按钮
    /// </summary>
    TextButton,

    /// <summary>
    ///     图标文本按钮
    /// </summary>
    IconAndTextButton,

    /// <summary>
    ///     文本菜单
    /// </summary>
    TextMenuItem,

    /// <summary>
    ///     图标文本菜单
    /// </summary>
    IconAndTextMenuItem
}