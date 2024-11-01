using A6ToolKits.Commands;

namespace A6ToolKits.Layout.Controls.ControlCommand;

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
public class CloseCommandHandler : CommandHandlerBase<CloseCommandDefinition>
{
    public override Task Run(CommandControlItem commandControlItem)
    {
        var controller = CoreService.Instance.Controller;
        controller?.StopApplication();
        return Task.CompletedTask;
    }
}

#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释