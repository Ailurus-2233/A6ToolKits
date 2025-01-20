namespace A6ToolKits.Services;

/// <summary>
///     服务接口, 用于标记一个类为服务
/// </summary>
public interface IService
{
    /// <summary>
    ///     初始化服务
    /// </summary>
    void Initialize();
    
    /// <summary>
    ///     模块加载流程
    /// </summary>
    public void OnLoad();
    
    /// <summary>
    ///     当服务被卸载时调用
    /// </summary>
    void OnUnload();
}