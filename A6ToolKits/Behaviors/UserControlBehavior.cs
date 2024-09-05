using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;

namespace A6ToolKits.Behaviors;

public class UserControlBehavior
{
    static UserControlBehavior()
    {
        LoadedCommandProperty.Changed.Subscribe(OnLoadedCommandChanged);
        ClickCommandProperty.Changed.Subscribe(OnClickCommandChanged);
    }

    #region LoadedCommand AttachedProperty

    public static readonly AttachedProperty<ICommand> LoadedCommandProperty =
        AvaloniaProperty.RegisterAttached<UserControlBehavior, Control, ICommand>("LoadedCommand");

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

    #endregion

    #region ClickCommand AttachedProperty

    public static readonly AttachedProperty<ICommand> ClickCommandProperty =
        AvaloniaProperty.RegisterAttached<UserControlBehavior, Control, ICommand>("ClickCommand");

    public static ICommand GetClickCommand(Control element)
    {
        return element.GetValue(ClickCommandProperty);
    }

    public static void SetClickCommand(Control element, ICommand value)
    {
        element.SetValue(ClickCommandProperty, value);
    }

    private static void OnClickCommandChanged(AvaloniaPropertyChangedEventArgs<ICommand> e)
    {
        if (e.Sender is not Control control) return;
        control.PointerPressed -= Control_Click;
        control.PointerPressed += Control_Click;
    }

    private static void Control_Click(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        if (sender is not Control control) return;
        var command = GetClickCommand(control);
        command?.Execute(null);
    }

    #endregion
}