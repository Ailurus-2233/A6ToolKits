using System;
using System.Collections.Generic;
using System.Xml;
using A6ToolKits.Helper.Config;
using Serilog;

namespace A6ToolKits.Module;

/// <summary>
///     模块加载帮助类
/// </summary>
public static class ModuleLoadHelper
{
    /// <summary>
    ///     获取需要加载的模块信息
    /// </summary>
    /// <returns></returns>
    public static Dictionary<string, ModuleConfigItem> GetModules()
    {
        // 加载模块配置文件
        var result = new Dictionary<string, ModuleConfigItem>();
        try
        {
            var modules = ConfigHelper.GetElements("Module");
            foreach (XmlNode module in modules!)
            {
                var item = new ModuleConfigItem();
                item.GenerateFromXmlNode(module);
                result.Add(item.Name, item);
            }
        }
        catch (Exception e)
        {
            Log.Error("Failed to load module configuration file: {0}", e.Message);
        }
        return result;
    }
    
    /// <summary>
    ///     
    /// </summary>
    /// <param name="module"></param>
    public static ModuleBase? CreateModule(ModuleConfigItem module) => 
        CoreService.Instance.Creator?.Create(module.Target, module.Assembly) as ModuleBase;
    
}