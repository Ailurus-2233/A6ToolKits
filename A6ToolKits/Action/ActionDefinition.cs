using System;
using System.Threading.Tasks;
using Avalonia.Media;

namespace A6ToolKits.Action;

public abstract class ActionDefinition
{
    public abstract string Text { get; }
    public abstract string ToolTip { get; }
    public abstract IImage ImageSource { get; }

    public bool CanRun { get; set; } = true;

    public EventHandler? CanRunChanged;
    
    public abstract Task Run();
}