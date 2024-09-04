using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using A6ToolKits.Common;
using Serilog;

namespace A6ToolKits.Module;

public static class ModuleLoader
{
    public static IEnumerable<ModuleBase> LoadModules()
    {
        Log.Information("Start loading modules.");

        var modules = new List<ModuleBase>();
        var moduleConfigs = GetModules();
        foreach (var module in moduleConfigs)
        {
            try
            {
                var assembly = Assembly.LoadFrom(module.Assembly);
                var type = assembly.GetTypes().FirstOrDefault(t => t.FullName == module.Target);
                var instance = (ModuleBase)Activator.CreateInstance(type!)!;
                instance.LoadModule();

                if (instance.ModuleVersion != module.Version)
                {
                    Log.Error("Module {0} version mismatch: {1} != {2}", module.Name, instance.ModuleVersion, module.Version);
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

    private static List<ModuleConfig> GetModules()
    {
        // 加载模块配置文件
        var moduleNames = new List<ModuleConfig>();
        try
        {
            var modules = ConfigReader.GetElements("Module");
            moduleNames.AddRange(from XmlElement module in modules
                let moduleName = module.GetAttribute("Name")
                let moduleVersion = module.GetAttribute("Version")
                let assembly = module.GetAttribute("Assembly")
                let target = module.GetAttribute("Target")
                select new ModuleConfig
                {
                    Name = moduleName, 
                    Version = moduleVersion, 
                    Assembly = assembly, 
                    Target = target
                });
        }
        catch (Exception e)
        {
            Log.Error("Failed to load module configuration file: {0}", e.Message);
        }

        return moduleNames;
    }

    private class ModuleConfig
    {
        public required string Name { get; set; }
        public required string Version { get; set; }
        public required string Assembly { get; set; }
        public required string Target { get; set; }
    }
}