using A6ToolKits.Modules;

namespace A6ToolKits.Module;

/// <summary>
///     模块管理器接口
/// </summary>
public interface IModuleManager
{
    /// <summary>
    ///     获取需要加载模块类型列表
    /// </summary>
    /// <returns>
    ///     需要加载的模块列表
    /// </returns>
    List<Type> GetToLoadModuleList();

    /// <summary>
    ///     获取已加载的模块
    /// </summary>
    /// <returns>
    ///     已加载的模块列表
    /// </returns>
    List<IModule> GetLoadedModules();
}