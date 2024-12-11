using A6ToolKits.Command;
using A6ToolKits.Common.ResourceLoader;
using Avalonia.Media;

namespace ToDoList.UIModules.LeftToolBarModule.Commands;

public class OpenTaskListCommand : CommandBase
{
    public override string? Name => "Open Task List";
    public override string? ToolTip { get; } = LanguageManager.GetString("OpenTaskListCommand_ToolTip");
    public override IImage? Image { get; } = ResourceHelper.LoadImage("TaskListIcon");

    public override Task Run()
    {
        Console.Write("Hello world");
        return Task.CompletedTask;
    }
}