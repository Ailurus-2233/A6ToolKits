using A6ToolKits.Bootstrapper;
using A6ToolKits.Command;
using A6ToolKits.Common.Container;
using A6ToolKits.Common.ResourceLoader;
using Avalonia.Media;

namespace A6ToolKits.Layout.Controls.ControlCommand;

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
public sealed class CloseCommand : CommandBase
{
    public override string? Name { get; } = "关闭程序";
    public override string? ToolTip { get; } = "关闭程序";
    public override IImage? Image { get; } = ResourceHelper.LoadImage("CloseIcon");

    public override Task Run()
    {
        var window = IoC.GetInstance<IWindowController>()?.GetMainWindow();
        window?.Close();
        return Task.CompletedTask;
    }
}

#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释