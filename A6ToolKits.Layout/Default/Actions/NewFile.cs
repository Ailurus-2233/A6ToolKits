using A6ToolKits.Layout.Generate;

namespace A6ToolKits.Layout.Default.Actions;

public class NewFile : MenuActionBase
{
    public override string? Name { get; init; } = "新建文件";

    public override async Task Run()
    {
        await Task.CompletedTask;
    }
}