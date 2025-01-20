using A6Toolkits.Configuration;

namespace A6ToolKits.Services;

/// <summary>
///     一个模块基类，用于记录模块的信息
/// </summary>
public abstract class ServiceBase<T> : ServiceBase where T : IConfigItem, new()
{
    /// <summary>
    ///     模块配置
    /// </summary>
    protected T Config { get; } = new T();


    /// <inheritdoc />
    public override void OnLoad()
    {
        Config.LoadConfig();
        Initialize();
    }

    /// <inheritdoc />
    public override void OnUnload()
    {
        Config.SaveConfig();
    }
}

/// <summary>
///     一个模块基类，用于记录模块的信息
/// </summary>
public abstract class ServiceBase : IService
{
    /// <inheritdoc />
    public abstract void Initialize();

    public virtual void OnLoad()
    {
    }

    public virtual void OnUnload()
    {
    }
}