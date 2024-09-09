using System.Globalization;
using A6ToolKits.Action;
using A6ToolKits.Layout.Attributes;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Styling;

namespace A6ToolKits.Layout.Generator;

public class ButtonGenerator : IControlGenerator<Button>
{
    public ButtonType Type { get; set; }

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
    /// 根据高度计算字体大小，使字符刚好可以填充按钮
    /// </summary>
    /// <param name="button"> 按钮 </param>
    /// <returns></returns>
    private double CalculateFontSize(Button button)
    {
        if (button.Content is not TextBlock text || text.Text == null) return double.NaN;

        var fontSize = text.FontSize;

        var formatText = new FormattedText(text.Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, Typeface.Default, fontSize,
            Brushes.Black);
        while (formatText.Width < button.Bounds.Height && formatText.Height < button.Bounds.Height)
        {
            formatText.SetFontSize(fontSize++);
        }

        return fontSize - 5;
    }
}