using A6ToolKits.Layout.Container;
using A6ToolKits.Layout.Helper;
using A6ToolKits.Module;
using Serilog;

namespace A6ToolKits.Layout;

public class LayoutModule : ModuleBase
{
    public override string ModuleName { get; set; } = "A6ToolKits.Layout";
    public override string ModuleVersion { get; set; } = "1.0.0";

    public override string ModuleDescription { get; set; } =
        "Add Layout capabilities to A6ToolKits to automatically load layout for main window";

    public static WindowLayout? WindowLayout { get; set; }
    
    public static event EventHandler? LoadLayoutCompleted;

    protected override void Initialize()
    {
        Log.Information("Load layout from configuration file");

        WindowLayout = GenerateHelper.LoadLayout();
        if (WindowLayout == null)
            throw new Exception("Failed to load layout configuration");

        Log.Information("Load layout success");
        LoadLayoutCompleted?.Invoke(this, EventArgs.Empty);
    }
}