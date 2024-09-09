using A6ToolKits.Action;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Styling;

namespace A6ToolKits.Layout.Generate;

public abstract class ButtonActionBase : ActionBase, IGenerateControl<Button>
{
    public virtual ButtonType Type { get; set; } = ButtonType.Text;

    public Button GenerateControl()
    {
        var button = new Button();
        button.Click += (_, _) => Run();
        CanRunChanged += (_, _) => button.IsEnabled = CanRun;
        SetStyles(button);
        return button;
    }

    private void SetStyles(Button button)
    {
        var styles = button.Styles;
        var style = new Style(x => x.Is<Button>());

        // 设置水平剧中
        style.Setters.Add(new Setter(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Center));
        var buttonSize = button.Height > button.Width ? button.Width : button.Height;

        var image = new Image
        {
            Source = Icon,
            Width = buttonSize - 2,
            Height = buttonSize - 2,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center
        };

        var textBlock = new TextBlock
        {
            Text = Name,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center
        };

        var initials = new TextBlock
        {
            Text = Name?.Substring(0, 1).ToUpper(),
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center
        };

        switch (Type)
        {
            case ButtonType.Icon:
                style.Setters.Add(new Setter(Layoutable.VerticalAlignmentProperty, HorizontalAlignment.Center));
                style.Setters.Add(new Setter(Layoutable.WidthProperty, buttonSize));
                style.Setters.Add(new Setter(Layoutable.HeightProperty, buttonSize));
                style.Setters.Add(new Setter(ContentControl.ContentProperty, image));
                break;
            case ButtonType.IconAndText:
                var stackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };
                stackPanel.Children.Add(image);
                stackPanel.Children.Add(textBlock);
                style.Setters.Add(new Setter(Layoutable.HeightProperty, buttonSize));
                style.Setters.Add(new Setter(ContentControl.ContentProperty, stackPanel));
                break;
            case ButtonType.Initials:
                style.Setters.Add(new Setter(ContentControl.ContentProperty, initials));
                style.Setters.Add(new Setter(Layoutable.HeightProperty, buttonSize));
                style.Setters.Add(new Setter(Layoutable.WidthProperty, buttonSize));
                break;
            case ButtonType.Text:
            default:
                style.Setters.Add(new Setter(ContentControl.ContentProperty, textBlock));
                style.Setters.Add(new Setter(Layoutable.HeightProperty, buttonSize));
                break;
        }
    }
}

public enum ButtonType
{
    Icon,
    Text,
    IconAndText,
    Initials
}