using A6ToolKits.Attributes.Layout;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;

namespace A6ToolKits.Commands;

/// <summary>
///     Button 生成器
/// </summary>
public static class ButtonGenerator
{
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
                        button.SetButtonSize(image);
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
                        button.SetButtonSize(image);
                };
                break;
            case ButtonType.Text:
            default:
                button.Content = textBlock;
                break;
        }
    }

    private static void SetButtonSize(this Button button, Image image)
    {
        var bounds = button.Bounds;
        button.Width = bounds.Height;
        button.Height = bounds.Height;
        button.Padding = new Thickness(button.Width * 0.19);
    }
}