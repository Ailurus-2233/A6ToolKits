using A6ToolKits.Configuration;
using A6ToolKits.Module.Exceptions;
using A6ToolKits.Modules;

namespace A6ToolKits.Module;

/// <summary>
///     一个模块基类，用于记录模块的信息
/// </summary>
public abstract class ModuleBase<T> : ModuleBase where T : IConfigItem, new()
{
    /// <summary>
    ///     模块配置
    /// </summary>
    protected T Config { get; } = new T();

    /// <summary>
    ///     加载模块
    /// </summary>
    /// <exception cref="LoadModuleException">
    ///     模块加载失败
    /// </exception>
    public override void LoadModule()
    {
        Config.LoadConfig();
        Initialize();
    }
}

/// <summary>
///     一个模块基类，用于记录模块的信息
/// </summary>
public abstract class ModuleBase : IModule
{
    /// <summary>
    ///     初始化，加载模块时执行的操作
    /// </summary>
    public abstract void Initialize();

    /// <summary>
    ///     加载模块
    /// </summary>
    /// <exception cref="LoadModuleException">
    ///     模块加载失败
    /// </exception>
    public virtual void LoadModule()
    {
        Initialize();
    }
}