using A6ToolKits.Helper.Config;

namespace A6ToolKits.Module;

/// <summary>
///     模块配置项
/// </summary>
public class ModuleConfigItem : ConfigItemBase
{
    /// <summary>
    ///    空构造函数
    /// </summary>
    public ModuleConfigItem()
    {
    }

    /// <summary>
    ///    构造函数
    /// </summary>
    /// <param name="name">
    ///     模块名称
    /// </param>
    /// <param name="version">
    ///     模块版本
    /// </param>
    /// <param name="assembly">
    ///     模块所在程序集
    /// </param>
    /// <param name="target">
    ///     目标模块类
    /// </param>
    public ModuleConfigItem(string name, string version, string assembly, string target)
    {
        Name = name;
        Version = version;
        Assembly = assembly;
        Target = target;
    }

    /// <summary>
    ///     模块名称，用于对照模块是否加载正确
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     模块版本，用于对照模块是否加载正确
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    ///     模块所在程序集，用于加载模块
    /// </summary>
    public string Assembly { get; set; } = string.Empty;

    /// <summary>
    ///     目标模块类，用于加载模块
    /// </summary>
    public string Target { get; set; } = string.Empty;
}