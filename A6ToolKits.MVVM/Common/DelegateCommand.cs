using System.Windows.Input;

namespace A6ToolKits.MVVM.Common;

public class DelegateCommand(Action<object> execute, Func<object, bool>? canExecute = null)
    : ICommand
{
    private readonly Func<object, bool> _canExecute = canExecute ?? (o => true);
    
    public bool CanExecute(object? parameter)
    {
        return parameter != null && _canExecute(parameter);
    }

    public void Execute(object? parameter)
    {
        if (parameter != null) execute(parameter);
    }

    public event EventHandler? CanExecuteChanged;
}