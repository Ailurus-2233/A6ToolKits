namespace A6ToolKits.Loader;

/// <summary>
///     加载器接口，用于在项目启动时自动加载配置文件中的模块和服务
/// </summary>
public interface ILoader
{
    /// <summary>
    ///     加载器的初始化方法
    /// </summary>
    void Initialize();
    
    /// <summary>
    ///     加载器的加载方法
    /// </summary>
    void Load();
    
    /// <summary>
    ///     加载器的完成方法
    /// </summary>
    void Complete();
}