using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace A6ToolKits.Layout.ActionMenu.Controls;

public class ActionMenuItem : MenuItem
{
    public static readonly StyledProperty<string> TextProperty =
        AvaloniaProperty.Register<ActionMenuItem, string>(nameof(Text));

    public static readonly StyledProperty<MenuActionDefinition> ActionProperty =
        AvaloniaProperty.Register<ActionMenuItem, MenuActionDefinition>(nameof(Action));


    public string Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public MenuActionDefinition Action
    {
        get => GetValue(ActionProperty);
        set => SetValue(ActionProperty, value);
    }

    public ActionMenuItem()
    {
        ActionProperty.Changed.AddClassHandler<ActionMenuItem>((_, e) =>
        {
            if (e.NewValue is not MenuActionDefinition action) return;
            Text = action.Text;
            Click += (_, _) => action.Run();
            IsEnabled = action.CanRun;
            action.CanRunChanged += (_, _) => IsEnabled = action.CanRun;
        });
    }
}