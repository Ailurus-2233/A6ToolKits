using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using A6ToolKits.Helper;
using Serilog;

namespace A6ToolKits.Module;

public static class ModuleLoader
{
    public static IList<ModuleBase> LoadModules()
    {
        Log.Information("Start loading modules.");

        var modules = new List<ModuleBase>();
        var moduleConfigs = GetModules();
        foreach (var module in moduleConfigs)
        {
            try
            {
                var instance = AssemblyHelper.CreateInstance<ModuleBase>(module.Assembly, module.Target);
                instance?.LoadModule();

                if (instance?.ModuleVersion != module.Version)
                {
                    Log.Error("Module {0} version mismatch: {1} != {2}", module.Name, instance?.ModuleVersion,
                        module.Version);
                    continue;
                }

                modules.Add(instance);
            }
            catch (Exception e)
            {
                Log.Error("Failed to load module {0}.{1}: {2}", module.Name, module.Version, e.Message);
            }
        }

        return modules;
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
}

public class ModuleConfigItem : ConfigItemBase
{
    public string Name { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string Assembly { get; set; } = string.Empty;
    public string Target { get; set; } = string.Empty;
}