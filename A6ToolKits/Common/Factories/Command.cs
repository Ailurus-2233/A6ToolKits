using System;
using System.Windows.Input;

namespace A6ToolKits.Common.Factories;

public class Command(Action execute, Func<bool>? canExecute = null) : ICommand
{
    private readonly Func<bool> _canExecute = canExecute ?? (() => true);
    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return _canExecute.Invoke();
    }

    public void Execute(object? parameter)
    {
        execute.Invoke();
    }

    public static ICommand Create(Action execute)
    {
        return new Command(execute);
    }

    public static ICommand Create(Action execute, Func<bool> canExecute)
    {
        return new Command(execute, canExecute);
    }

    public static ICommand Create<T>(Action<T> execute)
    {
        return new Command<T>(execute);
    }

    public static ICommand Create<T>(Action<T> execute, Func<T?, bool> canExecute)
    {
        return new Command<T>(execute, canExecute);
    }
}

public class Command<T>(Action<T> execute, Func<T, bool>? canExecute = null) : ICommand
{
    private readonly Func<T, bool> _canExecute = canExecute ?? (_ => true);
    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        if (parameter is T p) return _canExecute.Invoke(p);

        return false;
    }

    public void Execute(object? parameter)
    {
        if (parameter is T p) execute.Invoke(p);
    }
}