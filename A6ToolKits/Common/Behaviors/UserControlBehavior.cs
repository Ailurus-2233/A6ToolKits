using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;

namespace A6ToolKits.Common.Behaviors;

public class UserControlBehavior
{
    public static readonly AttachedProperty<ICommand> LoadedCommandProperty =
        AvaloniaProperty.RegisterAttached<UserControlBehavior, Control, ICommand>("LoadedCommand");

    static UserControlBehavior()
    {
        LoadedCommandProperty.Changed.Subscribe(OnLoadedCommandChanged);
    }

    public static ICommand GetLoadedCommand(Control element)
    {
        return element.GetValue(LoadedCommandProperty);
    }

    public static void SetLoadedCommand(Control element, ICommand value)
    {
        element.SetValue(LoadedCommandProperty, value);
    }

    private static void OnLoadedCommandChanged(AvaloniaPropertyChangedEventArgs<ICommand> e)
    {
        if (e.Sender is not Control control) return;
        control.AttachedToVisualTree -= Control_Loaded;
        control.AttachedToVisualTree += Control_Loaded;
    }

    private static void Control_Loaded(object? sender, VisualTreeAttachmentEventArgs e)
    {
        if (sender is not Control control) return;
        var command = GetLoadedCommand(control);
        command?.Execute(null);
    }
}