using A6Toolkits.Configuration;

namespace A6ToolKits.Modules;

/// <summary>
///     一个模块基类，用于记录模块的信息
/// </summary>
public abstract class ModuleBase<T> : ModuleBase where T : IConfigItem, new()
{
    /// <summary>
    ///     模块配置
    /// </summary>
    protected T Config { get; } = new T();


    /// <inheritdoc />
    public override void OnLoadModule()
    {
        Config.LoadConfig();
        Initialize();
    }

    /// <inheritdoc />
    public override void OnUnloadModule()
    {
        Config.SaveConfig();
    }
}

/// <summary>
///     一个模块基类，用于记录模块的信息
/// </summary>
public abstract class ModuleBase : IModule
{
    /// <inheritdoc />
    public abstract void Initialize();

    /// <inheritdoc />
    public virtual void OnLoadModule()
    {
        Initialize();
    }

    /// <inheritdoc />
    public virtual void OnUnloadModule()
    {
    }
}