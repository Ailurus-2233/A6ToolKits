using A6ToolKits.Action;

namespace A6ToolKits.Layout.ActionMenu;

public abstract class MenuActionDefinition : ActionDefinition
{
    public abstract IList<MenuActionDefinition> SubCommands { get; }

    public bool IsGroup => SubCommands.Count > 0;
    
}