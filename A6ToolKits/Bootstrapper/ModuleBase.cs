using A6ToolKits.Bootstrapper.Interfaces;

namespace A6ToolKits.Bootstrapper;

public class ModuleBase : IModule
{
    public string? ModuleName { get; set; }
    public string? ModuleVersion { get; set; }
    public string? ModuleDescription { get; set; }
}