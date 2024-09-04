namespace A6ToolKits.Module;

public class BaseModule : ModuleBase
{
    public override string ModuleName { get; set; } = "A6ToolKits.Root";
    public override string ModuleVersion { get; set; } = "1.0.0";
    public override string ModuleDescription { get; set; } = "Core of A6ToolKits framework";

    protected override void Initialize()
    {
    }
}