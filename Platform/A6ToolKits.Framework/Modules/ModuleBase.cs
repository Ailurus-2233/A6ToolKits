using A6ToolKits.Common.Exceptions;
using A6ToolKits.Config;
using A6ToolKits.Helper.Configurator;

namespace A6ToolKits.Modules;

/// <summary>
///     一个模块基类，用于记录模块的信息
/// </summary>
public abstract class ModuleBase<T> : ModuleBase, IModule where T : ConfigItemBase
{
    /// <summary>
    ///     模块配置
    /// </summary>
    protected abstract T Config { get; }
    
    /// <summary>
    ///     加载模块
    /// </summary>
    /// <exception cref="LoadModuleException">
    ///     模块加载失败
    /// </exception>
    public override void LoadModule()
    {
        try
        {
            try
            {
                Config.LoadConfig();
            } catch (ConfigLoadException)
            {
                ConfigHelper.AddXmlNodeToDefaultConfigFile(Config.CreateDefaultConfig());
                Config.SetDefault();
            }
            Initialize();
        }
        catch (Exception e)
        {
            throw new LoadModuleException(Name, e.StackTrace);
        }
    }
}

/// <summary>
///     一个模块基类，用于记录模块的信息
/// </summary>
public abstract class ModuleBase : IModule
{
    /// <summary>
    ///     模块名称
    /// </summary>
    protected abstract string Name { get; }

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
        try
        {
            Initialize();
        }
        catch (Exception e)
        {
            throw new LoadModuleException(Name, e.StackTrace);
        }
    }
}
