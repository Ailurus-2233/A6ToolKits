using A6ToolKits.Commands;
using A6ToolKits.Resource;
using Avalonia.Media;

namespace A6ToolKits.Layout.Controls.ControlCommand;

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
public sealed class WindowCommandDefinition : CommandDefinitionBase
{
    /// <inheritdoc />
    public override string? Name { get; } = "窗口化";

    /// <inheritdoc />
    public override string? ToolTip { get; } = "窗口化";

    public override Uri? IconSource { get; } = null;
    public override IImage Image { get; } = ResourceHelper.LoadImage("WindowIcon");
    
}

#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释