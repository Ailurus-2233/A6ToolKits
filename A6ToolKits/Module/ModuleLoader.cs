using System;
using System.Collections.Generic;
using System.Xml;
using A6ToolKits.Helper.Config;
using A6ToolKits.InstanceCreator;
using Serilog;

namespace A6ToolKits.Module;

/// <summary>
///     模块加载器
/// </summary>
public static class ModuleLoader
{
    private static List<ModuleBase> Modules { get; } = [];
    private static IInstanceCreator? Creator { get; set; } = new BaseInstanceCreator();

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
    public static bool TryGetModule<T>(out T? result) where T : ModuleBase
    {
        result = Modules.Find(m => m.GetType() == typeof(T)) as T;
        return result != null;
    }

    /// <summary>
    ///     全部模块加载完成事件
    /// </summary>
    public static event EventHandler? LoadModulesCompleted;

    /// <summary>
    ///     加载配置文件中的指定的模块
    /// </summary>
    public static void LoadModules()
    {
        Log.Information("Start loading modules.");

        Modules.Clear();
        var moduleConfigs = GetModules();

        // 优先加载 MVVMModule
        var mvvm = moduleConfigs.Find(m => string.Equals(m.Name, "A6ToolKits.MVVM", StringComparison.Ordinal));
        CreateModule(mvvm);
        moduleConfigs.Remove(mvvm!);

        // 最后加载 LayoutModule
        var layout = moduleConfigs.Find(m => string.Equals(m.Name, "A6ToolKits.Layout", StringComparison.Ordinal));
        moduleConfigs.Remove(layout!);

        foreach (var module in moduleConfigs)
            try
            {
                CreateModule(module);
            }
            catch (Exception e)
            {
                Log.Error("Failed to load module {0}.{1}", module.Name, module.Version);
                Log.Error("Exception: {0}", e.Message);
            }

        CreateModule(layout);

        LoadModulesCompleted?.Invoke(null, EventArgs.Empty);
    }

    private static List<ModuleConfigItem> GetModules()
    {
        // 加载模块配置文件
        var moduleNames = new List<ModuleConfigItem>();
        try
        {
            var modules = ConfigHelper.GetElements("Module");
            foreach (XmlNode module in modules!)
            {
                var item = new ModuleConfigItem();
                item.GenerateFromXmlNode(module);
                moduleNames.Add(item);
            }
        }
        catch (Exception e)
        {
            Log.Error("Failed to load module configuration file: {0}", e.Message);
        }

        return moduleNames;
    }

    private static void CreateModule(ModuleConfigItem? module)
    {
        if (module == null) return;
        if (Creator?.GetOrCreateInstance(module.Target, module.Assembly) is not ModuleBase instance) return;

        if (string.Equals(instance.ModuleName, "A6ToolKits.MVVM", StringComparison.Ordinal))
            Creator = instance.Creator;

        instance.Creator = Creator;
        instance.LoadModule();

        if (instance.ModuleVersion != module.Version)
            Log.Error("Module {0} version mismatch: {1} != {2}", module.Name, instance.ModuleVersion, module.Version);

        Modules.Add(instance);
    }
}