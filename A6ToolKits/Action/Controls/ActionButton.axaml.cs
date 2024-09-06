using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace A6ToolKits.Action.Controls;

public class ActionButton : Button
{
    public static readonly StyledProperty<string> TextProperty =
        AvaloniaProperty.Register<ActionButton, string>(nameof(Text));

    public static readonly StyledProperty<string> ToolTipProperty =
        AvaloniaProperty.Register<ActionButton, string>(nameof(ToolTip));

    public static readonly StyledProperty<IImage?> IconProperty =
        AvaloniaProperty.Register<ActionButton, IImage?>(nameof(Icon));

    public static readonly StyledProperty<ActionBase> ActionProperty =
        AvaloniaProperty.Register<ActionButton, ActionBase>(nameof(Action));

    public static readonly StyledProperty<double> ButtonSizeProperty =
        AvaloniaProperty.Register<ActionButton, double>(nameof(ButtonSize), 32);

    public static readonly StyledProperty<double> IconSizeProperty =
        AvaloniaProperty.Register<ActionButton, double>(nameof(IconSize), 30);

    public string Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public string ToolTip
    {
        get => GetValue(ToolTipProperty);
        set => SetValue(ToolTipProperty, value);
    }

    public IImage? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public ActionBase Action
    {
        get => GetValue(ActionProperty);
        set => SetValue(ActionProperty, value);
    }

    public double ButtonSize
    {
        get => GetValue(ButtonSizeProperty);
        set => SetValue(ButtonSizeProperty, value);
    }

    public double IconSize
    {
        get => GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
    }

    public ActionButton()
    {
        ActionProperty.Changed.AddClassHandler<ActionButton>((_, e) =>
        {
            if (e.NewValue is not ActionBase action) return;

            Text = action.Name;
            ToolTip = action.ToolTip;
            Icon = action.Icon;
            Click += (_, _) => action.Run();
            IsEnabled = action.CanRun;
            action.CanRunChanged += (_, _) => IsEnabled = action.CanRun;
        });
    }
}