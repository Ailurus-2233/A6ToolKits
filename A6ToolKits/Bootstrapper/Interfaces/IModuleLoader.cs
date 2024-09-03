using A6ToolKits.Bootstrapper.Utils;
using A6ToolKits.Common.Exceptions;

namespace A6ToolKits.Bootstrapper.Interfaces;

/// <summary>
/// 一个模块加载器，用于子模块的初始化、配置和加载
/// </summary>
public abstract class ModuleLoader<T> where T : IModule
{
    /// <summary>
    /// 初始化当前模块
    /// </summary>
    protected abstract void Initialize(T module);

    /// <summary>
    /// 配置模块的程序集
    /// </summary>
    protected abstract void ConfigureAssembly(T module);

    /// <summary>
    /// 配置当前模块
    /// </summary>
    protected abstract void Configure(T module);

    /// <summary>
    /// 加载并返回当前模块
    /// </summary>
    /// <returns>
    /// 指定模块实例
    /// </returns>
    public virtual T LoadModule()
    {
        var module = ModuleInformationLoader.LoadModule<T>();
        if (module == null) throw new LoadModuleException("无法加载模块配置文件");
        try
        {
            Initialize(module);
            ConfigureAssembly(module);
            Configure(module);
            return module;
        }
        catch
        {
            throw new LoadModuleException("模块加载失败");
        }
    }
}