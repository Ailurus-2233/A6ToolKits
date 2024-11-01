using A6ToolKits.Commands;
using A6ToolKits.Resource;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace A6ToolKits.Layout.Controls.ControlCommand;

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
public class CloseCommandDefinition : CommandDefinitionBase
{
    public override string? Name { get; } = "关闭程序";
    public override string? ToolTip { get; } = "关闭程序";
    public override Uri? IconSource { get; } = null;
    public override IImage Image { get; } = ResourceHelper.LoadImage("CloseIcon");
}

#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释