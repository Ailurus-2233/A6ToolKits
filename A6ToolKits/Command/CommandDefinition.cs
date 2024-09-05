using System;
using System.Threading.Tasks;
using A6ToolKits.Command.Controls;
using Avalonia.Media;

namespace A6ToolKits.Command;

public abstract class CommandDefinition
{
    public abstract string Text { get; }
    public abstract string ToolTip { get; }
    public abstract IImage ImageSource { get; }

    public bool CanRun { get; set; } = true;

    public EventHandler? CanRunChanged;
    
    public abstract Task Run();
}