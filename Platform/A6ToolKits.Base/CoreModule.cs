using System;
using A6ToolKits.Bootstrapper;
using A6ToolKits.Common.Attributes;
using A6ToolKits.Common.Exceptions;
using A6ToolKits.Modules;

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
    protected override string Name => "A6ToolKits.Base";

    /// <summary>
    ///     初始化，加载模块时执行的操作
    /// </summary>
    public override void Initialize()
    {
        LoadModules();
    }

    private static void LoadModules()
    {
        var moduleList = IoC.GetInstance<IModuleManager>()?.GetModulesType();
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
                if (!module.IsSubclassOf(typeof(ModuleBase))) return;
                var target = IoC.GetInstance(module) as ModuleBase;
                target?.LoadModule();
            }
            catch (Exception e)
            {
                throw new LoadModuleException(module.Name, e.StackTrace);
            }
        });

        if (layout == null) return;

        var target = IoC.GetInstance(layout) as ModuleBase;
        target?.LoadModule();
    }
}