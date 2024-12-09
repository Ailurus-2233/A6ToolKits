using A6ToolKits.Module;

namespace A6ToolKits.Database;

/// <summary>
///     数据库模块
/// </summary>
public interface IDatabaseModule: IModule
{
    /// <summary>
    ///     根据 ID 获取数据库管理器
    /// </summary>
    /// <param name="id">
    ///     数据库管理器 ID
    /// </param>
    /// <returns>
    ///     数据库管理器
    /// </returns>
    IManager? GetDatabaseManger(string id);

    /// <summary>
    ///     根据 ID 获取数据库管理器
    /// </summary>
    /// <param name="id">
    ///     数据库管理器 ID
    /// </param>
    /// <typeparam name="T">
    ///     数据库管理器类型
    /// </typeparam>
    /// <returns>
    ///     数据库管理器
    /// </returns>
    T GetDatabaseManager<T>(string id) where T : IManager;
}