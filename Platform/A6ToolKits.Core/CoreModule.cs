using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
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
    public override string Name => "A6ToolKits.Core";

    /// <summary>
    ///     初始化，加载模块时执行的操作
    /// </summary>
    public override void Initialize()
    {
        Log.Information("Start loading modules.");
        var moduleList = CoreService.Instance.Controller?.LoadModules();
        if (moduleList is { Count: > 0 })
        {
            // 优先加载 MVVMModule
            var mvvm = moduleList.Find(t => t.Name == "MVVMModule");
            if (mvvm != null)
                CoreService.Instance.Creator?.Create(mvvm);
            // 最后加载 LayoutModule
            var layout = moduleList.Find(t => t.Name == "LayoutModule");
            
            moduleList.ForEach(module =>
            {
                if (module == mvvm || module == layout)
                    return;
                try
                {
                    // 判断 module 是否是 ModuleBase 的基类
                    if (module.IsSubclassOf(typeof(ModuleBase)))
                        CoreService.Instance.Creator?.Create(module);
                    else
                        Log.Warning("Module {0} is not a subclass of ModuleBase", module.Name);
                }
                catch (Exception e)
                {
                    Log.Error("Failed to load module {0}.{1}", module.Name);
                    Log.Error("Exception: {0}", e.Message);
                }
            });
            if (layout != null)
                CoreService.Instance.Creator?.Create(layout);
        }
    }
}