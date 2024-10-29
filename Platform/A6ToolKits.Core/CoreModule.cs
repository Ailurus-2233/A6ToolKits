using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using A6ToolKits.Common.Attributes.MVVM;
using A6ToolKits.Helper.Config;
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
    ///     已加载的模块列表
    /// </summary>
    private readonly List<ModuleBase> _modules = [];

    /// <summary>
    ///     初始化，加载模块时执行的操作
    /// </summary>
    public override void Initialize()
    {
        Log.Information("Start loading modules.");
        _modules.Clear();
        var moduleConfigs = ModuleLoadHelper.GetModules();
        var moduleList = moduleConfigs.Values.ToList();
        
        // 优先加载 MVVMModule
        var mvvmConfigItem = moduleConfigs.GetValueOrDefault("A6ToolKits.MVVM");
        CreateModule(mvvmConfigItem);

        // 最后加载 LayoutModule
        var layoutConfigItem = moduleConfigs.GetValueOrDefault("A6ToolKits.Layout");
        if (layoutConfigItem != null)
            moduleList.Remove(layoutConfigItem);

        moduleList.ForEach(module =>
        {
            try
            {
                CreateModule(module);
            }
            catch (Exception e)
            {
                Log.Error("Failed to load module {0}.{1}", module.Name, module.Version);
                Log.Error("Exception: {0}", e.Message);
            }
        });
        CreateModule(layoutConfigItem);
        return;

        void CreateModule(ModuleConfigItem? module)
        {
            if (module == null) return;
            var instance = ModuleLoadHelper.CreateModule(module);
            moduleList.Remove(module!);
            if (instance != null)
                _modules.Add(instance);
        }
    }

    /// <summary>
    ///     尝试获取模块，会从当前已加载的模块中查找是否存在指定类型的模块
    /// </summary>
    /// <param name="result">
    ///     模块实例
    /// </param>
    /// <typeparam name="T">
    ///     模块类型
    /// </typeparam>
    /// <returns>
    ///     是否获取成功
    /// </returns>
    public bool TryGetModule<T>(out T? result) where T : ModuleBase
    {
        result = _modules.Find(m => m.GetType() == typeof(T)) as T;
        return result != null;
    }
}