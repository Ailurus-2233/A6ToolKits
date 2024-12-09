namespace A6ToolKits.Services;

/// <summary>
///     服务管理器
/// </summary>
public interface IServiceManager
{
    /// <summary>
    ///     获取需要加载模块类型列表
    /// </summary>
    /// <returns>
    ///     需要加载的模块列表
    /// </returns>
    List<Type> GetToLoadServiceList();

    /// <summary>
    ///     获取已加载的模块
    /// </summary>
    /// <returns>
    ///     已加载的模块列表
    /// </returns>
    List<IService> GetLoadedServices();
}