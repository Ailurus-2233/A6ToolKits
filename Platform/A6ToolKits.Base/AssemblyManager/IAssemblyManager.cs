namespace A6ToolKits.AssemblyManager;

/// <summary>
///     程序集管理器
/// </summary>
public interface IAssemblyManager
{
    /// <summary>
    ///     获取加载程序集路径
    /// </summary>
    /// <returns>
    ///     程序集加载路径列表
    /// </returns>
    List<string> GetAssemblyPaths();
}