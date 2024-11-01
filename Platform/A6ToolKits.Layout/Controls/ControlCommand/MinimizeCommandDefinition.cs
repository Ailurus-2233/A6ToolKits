using A6ToolKits.Commands;
using A6ToolKits.Resource;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace A6ToolKits.Layout.Controls.ControlCommand;

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
public class MinimizeCommandDefinition : CommandDefinitionBase
{
    public override string? Name { get; } = "最小化";
    public override string? ToolTip { get; } = "最小化";
    public override Uri? IconSource { get; } = null;
    public override IImage Image { get; } = ResourceHelper.LoadImage("MinusIcon");
}

#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释