using System;
using System.Linq;
using System.Reflection;
using A6ToolKits.Common.Attributes.MVVM;
using A6ToolKits.Exceptions;
using Serilog;

namespace A6ToolKits.Module;

/// <summary>
///     一个模块基类，用于记录模块的信息
/// </summary>
public abstract class ModuleBase:IModule
{
    /// <summary>
    ///     模块名称
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    ///     构造函数
    /// </summary>
    protected ModuleBase()
    {
        LoadModule();
    }
    
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
    public void LoadModule()
    {
        try {
            Log.Information($"Loading module {Name}");
            Initialize();
            Log.Information($"Module {Name} loaded successfully");
        }
        catch (Exception e)
        {
            throw new LoadModuleException($"Module load failed: {e.Message}");
        }
    }
}