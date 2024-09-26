using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;

namespace A6ToolKits.Behaviors;

/// <summary>
///     用户控件行为
/// </summary>
public class UserControlBehavior
{
    static UserControlBehavior()
    {
        LoadedCommandProperty.Changed.Subscribe(OnLoadedCommandChanged);
        ClickCommandProperty.Changed.Subscribe(OnClickCommandChanged);
    }

    #region LoadedCommand AttachedProperty

    /// <summary>
    ///     控件加载时执行的命令
    /// </summary>
    public static readonly AttachedProperty<ICommand> LoadedCommandProperty =
        AvaloniaProperty.RegisterAttached<UserControlBehavior, Control, ICommand>("LoadedCommand");

    /// <summary>
    ///     获取加载命令
    /// </summary>
    /// <param name="element">
    ///     控件
    /// </param>
    /// <returns>
    ///     返回加载命令
    /// </returns>
    public static ICommand GetLoadedCommand(Control element)
    {
        return element.GetValue(LoadedCommandProperty);
    }

    /// <summary>
    ///     设置加载命令
    /// </summary>
    /// <param name="element">
    ///     控件
    /// </param>
    /// <param name="value">
    ///     加载命令
    /// </param>
    public static void SetLoadedCommand(Control element, ICommand value)
    {
        element.SetValue(LoadedCommandProperty, value);
    }

    private static void OnLoadedCommandChanged(AvaloniaPropertyChangedEventArgs<ICommand> e)
    {
        if (e.Sender is not Control control) return;
        control.AttachedToVisualTree -= ControlLoaded;
        control.AttachedToVisualTree += ControlLoaded;
    }

    private static void ControlLoaded(object? sender, VisualTreeAttachmentEventArgs e)
    {
        if (sender is not Control control) return;
        var command = GetLoadedCommand(control);
        command?.Execute(null);
    }

    #endregion

    #region ClickCommand AttachedProperty

    /// <summary>
    ///     点击时执行的命令
    /// </summary>
    public static readonly AttachedProperty<ICommand> ClickCommandProperty =
        AvaloniaProperty.RegisterAttached<UserControlBehavior, Control, ICommand>("ClickCommand");

    /// <summary>
    ///     获取点击命令
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    public static ICommand GetClickCommand(Control element)
    {
        return element.GetValue(ClickCommandProperty);
    }

    /// <summary>
    ///     设置点击命令
    /// </summary>
    /// <param name="element">
    ///     控件
    /// </param>
    /// <param name="value">
    ///     命令
    /// </param>
    public static void SetClickCommand(Control element, ICommand value)
    {
        element.SetValue(ClickCommandProperty, value);
    }

    private static void OnClickCommandChanged(AvaloniaPropertyChangedEventArgs<ICommand> e)
    {
        if (e.Sender is not Control control) return;
        control.PointerPressed -= ControlClick;
        control.PointerPressed += ControlClick;
    }

    private static void ControlClick(object? sender, PointerPressedEventArgs e)
    {
        if (sender is not Control control) return;
        var command = GetClickCommand(control);
        command?.Execute(null);
    }

    #endregion
}