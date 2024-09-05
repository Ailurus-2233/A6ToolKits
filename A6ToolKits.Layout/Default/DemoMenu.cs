using A6ToolKits.Layout.ActionMenu;
using A6ToolKits.Layout.Default.DemoMenuAction;

namespace A6ToolKits.Layout.Default;

public class DemoMenu
{
    public List<MenuActionDefinition> MenuActions { get; } = [new FileGroup()];
}