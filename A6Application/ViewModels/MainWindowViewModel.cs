using System.Collections.Generic;
using System.Collections.ObjectModel;
using A6Application.Commands;
using A6Application.Views;
using A6ToolKits.Command;
using A6ToolKits.MVVM.Common;
using A6ToolKits.MVVM.Common.Attributes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace A6Application.ViewModels;

[TargetView(typeof(MainWindow))]
public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private string _greeting = "Hello World!";

    [ObservableProperty] private bool _canClick = true;
    
    [ObservableProperty] private ObservableCollection<CommandDefinition> _commands = [];


    [RelayCommand(CanExecute = nameof(CanClick))]
    private void Click()
    {
        Greeting = "Hello A6ToolKits!";
        CanClick = false;
        ClickCommand.NotifyCanExecuteChanged();
    }

    public MainWindowViewModel()
    {
        _commands.Add(new OpenWindowCommand());
    }
}