using A6ToolKits.Modules;
namespace A6ToolKits.Loader;

/// <summary>
///     模块加载器，用于加载配置文件中的模块
/// </summary>
public class ModuleLoader : ILoader
{
    private IList<IModule>? _modules;
    
    /// <summary>
    ///     初始化模块加载器，从配置文件中读取加载模块的具体目标
    /// </summary>
    public void Initialize()
    {
        _modules = new List<IModule>();
    }
    
    /// <summary>
    ///     加载模块，依次加载 _modules 中的模块
    /// </summary>
    public void Load()
    {
        
    }
    
    /// <summary>
    ///     完成模块加载
    /// </summary>
    public void Complete()
    {
        
    }
}