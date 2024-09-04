using System;
using Serilog;

namespace A6ToolKits.Module;

/// <summary>
/// 一个模块接口，用于记录模块的信息
/// </summary>
public abstract class ModuleBase
{
    public abstract string ModuleName { get; set; }
    public abstract string ModuleVersion { get; set; }
    public abstract string ModuleDescription { get; set; }

    protected abstract void Initialize();

    public void LoadModule()
    {
        try
        {
            Log.Information($"Loading module {ModuleName} {ModuleVersion}");
            Initialize();
            Log.Information($"Module {ModuleName} {ModuleVersion} loaded successfully");
        }
        catch (Exception e)
        {
            Log.Error(e.Message, "Module load failed");
            throw new Common.Exceptions.LoadModuleException("Module load failed");
        }
    }
}