using System;
using A6ToolKits.Bootstrapper.Interfaces;
using A6ToolKits.Common.Attributes.MVVM;
using A6ToolKits.Module;
using Serilog;

namespace A6ToolKits;

/// <summary>
///     核心模块，首先加载的模块，并负责后续模块的加载
/// </summary>
[AutoRegister(typeof(CoreModule), RegisterType.Singleton)]
public class CoreModule : ModuleBase
{
    /// <summary>
    ///     模块名称
    /// </summary>
    protected override string Name => "A6ToolKits.Core";

    /// <summary>
    ///     初始化，加载模块时执行的操作
    /// </summary>
    public override void Initialize()
    {
        LoadModules();
    }

    private static void LoadModules()
    {
        Log.Information("Start loading modules.");
        var moduleList = IoC.GetInstance<IApplicationController>()?.LoadModules();
        if (moduleList is not { Count: > 0 }) return;
        // 最后加载 LayoutModule
        var layout = moduleList.Find(t => t.Name == "LayoutModule");

        moduleList.ForEach(module =>
        {
            if (module == layout)
                return;
            try
            {
                // 判断 module 是否是 ModuleBase 的基类
                if (module.IsSubclassOf(typeof(ModuleBase)))
                {
                    var target = IoC.GetInstance(module) as ModuleBase;
                    target?.LoadModule();
                }
                else
                {
                    Log.Warning("Module {0} is not a subclass of ModuleBase", module.Name);
                }
            }
            catch (Exception e)
            {
                Log.Error("Failed to load module {0}", module.Name);
                Log.Error("Exception: {0}", e.Message);
            }
        });

        if (layout == null) return;

        var target = IoC.GetInstance(layout) as ModuleBase;
        target?.LoadModule();
    }
}