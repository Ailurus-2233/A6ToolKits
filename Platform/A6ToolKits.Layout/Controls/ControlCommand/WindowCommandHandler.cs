using A6ToolKits.Commands;
using Avalonia.Controls;

namespace A6ToolKits.Layout.Controls.ControlCommand;

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
public class WindowCommandHandler : CommandHandlerBase<WindowCommandDefinition>
{
    public override Task Run(CommandControlItem commandControlItem)
    {
        var window = CoreService.Instance.Controller?.GetMainWindow();
        if (window is null) return Task.CompletedTask;
        window.WindowState = WindowState.Normal;
        return Task.CompletedTask;
    }
}

#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释