using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace A6ToolKits.Action.Controls;

public class ActionMenuItem : MenuItem
{
    public static readonly StyledProperty<ActionBase> ActionProperty =
        AvaloniaProperty.Register<ActionMenuItem, ActionBase>(nameof(Action));

    public static readonly StyledProperty<double> IconSizeProperty =
        AvaloniaProperty.Register<ActionButton, double>(nameof(IconSize), 24);

    public ActionBase Action
    {
        get => GetValue(ActionProperty);
        set => SetValue(ActionProperty, value);
    }

    public double IconSize
    {
        get => GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
    }

    public ActionMenuItem()
    {
        ActionProperty.Changed.AddClassHandler<ActionMenuItem>((_, e) =>
        {
            if (e.NewValue is not ActionBase action) return;

            Header = action.Name;
            Icon = action.Icon;
            IsEnabled = action.CanRun;
            action.CanRunChanged += (_, _) => IsEnabled = action.CanRun;
            Click += (_, _) => { action.Run(); };
        });
    }
}