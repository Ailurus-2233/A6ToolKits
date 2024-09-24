using System.Globalization;
using A6ToolKits.Action;
using A6ToolKits.Layout.Helper.Interfaces;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace A6ToolKits.Layout.Helper;

/// <summary>
///     按钮生成器，针对不同的按钮类型生成不同的按钮
/// </summary>
public class ButtonGenerateHelper : IControlGenerateHelper<Button>
{
    /// <summary>
    ///     按钮类型，默认为 Icon
    /// </summary>
    public ButtonType Type { get; set; }

    /// <summary>
    ///     基于 ActionBase 生成按钮
    /// </summary>
    /// <param name="action">
    ///     按钮执行的动作
    /// </param>
    /// <returns>
    ///     生成的按钮控件
    /// </returns>
    public Button GenerateControl(ActionBase action)
    {
        var button = new Button();
        button.Click += (_, _) => action.Run();
        action.CanRunChanged += (_, _) => button.IsEnabled = action.CanRun;
        button.VerticalAlignment = VerticalAlignment.Stretch;
        SetTemplate(button, action);
        button.Margin = new Thickness(5);
        button.Padding = new Thickness(0);

        // 增加 ToolTip
        var toolTip = new ToolTip
        {
            Content = action.ToolTip
        };
        ToolTip.SetTip(button, toolTip);

        return button;
    }

    private void SetTemplate(Button button, ActionBase action)
    {
        // 设置水平剧中
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

        switch (Type)
        {
            case ButtonType.Icon:
                button.Content = image;
                button.Loaded += (_, _) =>
                {
                    var bounds = button.Bounds;
                    button.Width = bounds.Height;
                    button.Height = bounds.Height;
                    image.Width = bounds.Height - 2;
                    image.Height = bounds.Height - 2;
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
                button.Loaded += (_, _) =>
                {
                    var bounds = button.Bounds;
                    button.Width = bounds.Height;
                    button.Height = bounds.Height;
                    var fontSize = CalculateFontSize(button);
                    initials.FontSize = fontSize;
                };
                break;
            case ButtonType.Text:
            default:
                button.Content = textBlock;
                break;
        }
    }

    /// <summary>
    ///     根据高度计算字体大小，使字符刚好可以填充按钮
    /// </summary>
    /// <param name="button"> 按钮 </param>
    /// <returns></returns>
    private double CalculateFontSize(Button button)
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
}

/// <summary>
///     按钮类型
/// </summary>
public enum ButtonType
{
    /// <summary>
    ///     仅图标
    /// </summary>
    Icon,

    /// <summary>
    ///     仅文字
    /// </summary>
    Text,

    /// <summary>
    ///     图标和文字
    /// </summary>
    IconAndText,

    /// <summary>
    ///     首字母
    /// </summary>
    Initials
}