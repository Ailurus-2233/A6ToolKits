using A6ToolKits.Bootstrapper.Interfaces;
using static System.Text.Json.JsonSerializer;

namespace A6ToolKits.Bootstrapper.Utils;

/// <summary>
/// 模块信息加载器，加载的目标文件为json文件
/// </summary>
public static class ModuleInformationLoader
{
    public static T? LoadModule<T>(string path = "module.json") where T : IModule
    {
        var text = System.IO.File.ReadAllText(path);
        var module = Deserialize<T>(text);
        return module;
    }
}