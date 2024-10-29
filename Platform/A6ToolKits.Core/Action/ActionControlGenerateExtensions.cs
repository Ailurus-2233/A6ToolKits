using System.Globalization;
using A6ToolKits.Common.Attributes.Layout;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace A6ToolKits.Common.Action;

/// <summary>
///     扩展 ActionBase 使其能够生成一些控件
/// </summary>
public static class ActionControlGenerateExtensions
{
    #region MenuItem

    /// <summary>
    ///     基于 ActionBase 生成一个菜单项，用于显示在菜单中
    /// </summary>
    /// <returns>
    ///     生成的菜单项
    /// </returns>
    public static MenuItem GenerateMenuItem(this ActionBase action)
    {
        var menuItem = new MenuItem
        {
            Header = action.Name,
            IsEnabled = action.CanRun,
            Icon = action.Icon
        };

        menuItem.Click += (_, _) => action.Run();
        action.CanRunChanged += (_, _) => menuItem.IsEnabled = action.CanRun;

        return menuItem;
    }

    #endregion

    #region Button

    /// <summary>
    ///     基于 ActionBase 生成按钮
    /// </summary>
    /// <param name="action">
    ///     按钮执行的动作
    /// </param>
    /// <param name="type">
    ///     生成按钮的类型
    /// </param>
    /// <returns>
    ///     生成的按钮控件
    /// </returns>
    public static Button GenerateButton(this ActionBase action, ButtonType type)
    {
        var button = new Button();
        button.Click += (_, _) => action.Run();
        action.CanRunChanged += (_, _) => button.IsEnabled = action.CanRun;
        button.IsEnabled = action.CanRun;
        button.SetTemplate(action, type);
        button.Margin = new Thickness(5);
        button.Padding = new Thickness(0);
        
        button.VerticalAlignment = VerticalAlignment.Center;
        button.HorizontalAlignment = HorizontalAlignment.Center;
        button.HorizontalContentAlignment = HorizontalAlignment.Stretch;
        button.VerticalContentAlignment = VerticalAlignment.Stretch;

        // 增加 ToolTip
        var toolTip = new ToolTip
        {
            Content = action.ToolTip
        };
        ToolTip.SetTip(button, toolTip);

        return button;
    }

    private static void SetTemplate(this Button button, ActionBase action, ButtonType type)
    {
        // 设置水平居中
        button.HorizontalAlignment = HorizontalAlignment.Center;

        var image = new Image
        {
            Source = action.Icon,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center
        };

        var textBlock = new TextBlock
        {
            Text = action.Name,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center
        };

        var initials = new TextBlock
        {
            Text = action.Name?[..1]?.ToUpper(),
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center
        };

        switch (type)
        {
            case ButtonType.Icon:
                button.Content = image;
                button.PropertyChanged += (_, args) =>
                {
                    if (args.Property.Name is "Bounds" or "IsEnabled" or "IsVisible")
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
                break;
            case ButtonType.Initials:
                button.Content = initials;
                button.PropertyChanged += (_, args) =>
                {
                    if (args.Property.Name is "Bounds" or "IsEnabled" or "IsVisible")
                        button.SetButtonSize(initials);
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
        image.Width = bounds.Height - 2;
        image.Height = bounds.Height - 2;
    }

    private static void SetButtonSize(this Button button, TextBlock initials)
    {
        var bounds = button.Bounds;
        button.Width = bounds.Height;
        button.Height = bounds.Height;
        var fontSize = button.CalculateFontSize();
        initials.FontSize = fontSize;
    }

    /// <summary>
    ///     根据高度计算字体大小，使字符刚好可以填充按钮
    /// </summary>
    /// <param name="button"> 按钮 </param>
    /// <returns> 字体大小 </returns>
    private static double CalculateFontSize(this Button button)
    {
        if (button.Content is not TextBlock text || text.Text == null) return double.NaN;

        var fontSize = text.FontSize;

        var formatText = new FormattedText(text.Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
            Typeface.Default, fontSize,
            Brushes.Black);
        while (formatText.Width < button.Bounds.Height && formatText.Height < button.Bounds.Height)
            formatText.SetFontSize(fontSize++);

        return fontSize - 5;
    }

    #endregion
}