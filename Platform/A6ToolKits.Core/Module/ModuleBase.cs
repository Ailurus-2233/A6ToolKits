using System;
using System.Linq;
using System.Reflection;
using A6ToolKits.Common.Attributes.MVVM;
using A6ToolKits.Helper.Instance;
using A6ToolKits.Module.Exceptions;
using Serilog;

namespace A6ToolKits.Module;

/// <summary>
///     一个模块接口，用于记录模块的信息
/// </summary>
public abstract class ModuleBase
{
    /// <summary>
    ///     实例创建器，用于模块内部创建实例
    /// </summary>
    public virtual IInstanceHelper? Creator { get; set; } = new BaseInstanceHelper();

    /// <summary>
    ///     模块名称
    /// </summary>
    public string? ModuleName { get; set; }

    /// <summary>
    ///     模块版本
    /// </summary>
    public string? ModuleVersion { get; set; }

    /// <summary>
    ///     模块描述
    /// </summary>
    public string? ModuleDescription { get; set; }
    

    /// <summary>
    ///     初始化，加载模块时执行的操作
    /// </summary>
    protected abstract void Initialize();

    /// <summary>
    ///     模块加载完成事件
    /// </summary>
    public event EventHandler? LoadModuleCompleted;

    /// <summary>
    ///     加载模块，加载完成后触发 <see cref="LoadModuleCompleted" />
    /// </summary>
    /// <exception cref="LoadModuleException">
    ///     模块加载失败
    /// </exception>
    public void LoadModule()
    {
        try {
            GetModuleInfo();
            Log.Information($"Loading module {ModuleName} {ModuleVersion}");
            Initialize();
            Log.Information($"Module {ModuleName} {ModuleVersion} loaded successfully");
        }
        catch (Exception e)
        {
            throw new LoadModuleException($"Module load failed: {e.Message}");
        }

        LoadModuleCompleted?.Invoke(this, EventArgs.Empty);
    }
    
    /// <summary>
    ///    获取模块信息
    /// </summary>
    private void GetModuleInfo()
    {
        var assembly = GetType().Assembly;
        var title = assembly.GetCustomAttributes<AssemblyTitleAttribute>().ToList();
        ModuleName = title.FirstOrDefault()?.Title ?? "Unknown";
        
        var version = assembly.GetCustomAttributes<AssemblyFileVersionAttribute>().ToList();
        ModuleVersion = version.FirstOrDefault()?.Version ?? "Unknown";
        
        var description = assembly.GetCustomAttributes<AssemblyDescriptionAttribute>().ToList();
        ModuleDescription = description.FirstOrDefault()?.Description ?? "Unknown";
    }
}