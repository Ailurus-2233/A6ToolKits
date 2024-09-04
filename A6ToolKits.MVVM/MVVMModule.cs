using A6ToolKits.Module;
using A6ToolKits.MVVM.Common;
using A6ToolKits.MVVM.Utils;

namespace A6ToolKits.MVVM;

public class MVVMModule : ModuleBase
{
    public override required string ModuleName { get; set; } = "A6ToolKits.MVVM";
    public override required string ModuleVersion { get; set; } = "1.0.0";

    public override required string ModuleDescription { get; set; } =
        "Add MVVM, IOC capabilities to A6ToolKits to automatically load some services and view & Viewmodel";

    protected override void Initialize()
    {
        IoCHelper.AutoRegisterAll();
    }
}