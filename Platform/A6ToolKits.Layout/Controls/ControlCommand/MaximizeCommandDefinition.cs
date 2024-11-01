using A6ToolKits.Commands;
using A6ToolKits.Event;
using A6ToolKits.Resource;
using Avalonia.Controls;
using Avalonia.Media;

namespace A6ToolKits.Layout.Controls.ControlCommand;

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
public sealed class MaximizeCommandDefinition : CommandDefinitionBase
{
    /// <inheritdoc />
    public override string? Name { get; } = "最大化";

    /// <inheritdoc />
    public override string? ToolTip { get; } = "最大化";

    public override Uri? IconSource { get; } = null;
    public override IImage Image { get; } = ResourceHelper.LoadImage("MaximizeIcon");
}

#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释