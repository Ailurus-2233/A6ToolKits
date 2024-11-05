using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using A6ToolKits.Config;
using Serilog;
using SysAssembly = System.Reflection.Assembly;

namespace A6ToolKits.AssemblyPath;

/// <summary>
///     程序集帮助类
/// </summary>
public static class AssemblyPathHelper
{
    private static List<string> _assemblyPaths { get; } = ["./"];

    /// <summary>
    ///     根据程序集名称和类型名称加载类型
    /// </summary>
    /// <param name="type">
    ///     类型名称
    /// </param>
    /// <param name="assembly">
    ///     程序集名称
    /// </param>
    /// <returns>
    ///     返回加载的类型
    /// </returns>
    public static Type? LoadType(string type, string? assembly = null)
    {
        if (assembly == null) return Type.GetType(type);
        SysAssembly? sysAssembly = null;
        _assemblyPaths.ForEach(path =>
        {
            var assemblyPath = Path.Combine(path, assembly);
            if (File.Exists(assemblyPath)) sysAssembly = SysAssembly.LoadFrom(assemblyPath);
        });
        var result = sysAssembly?.GetTypes().FirstOrDefault(t => t.FullName == type);
        return result;
    }

    /// <summary>
    ///     增加在 .net 加载程序集时的方法
    /// </summary>
    /// <param name="sender">
    ///     事件源
    /// </param>
    /// <param name="args">
    ///     事件参数
    /// </param>
    /// <returns>
    ///     返回加载的程序集
    /// </returns>
    public static SysAssembly? OnResolveAssembly(object? sender, ResolveEventArgs args)
    {
        var assemblyName = $"{new AssemblyName(args.Name).Name}.dll";
        SysAssembly? assembly = null;
        _assemblyPaths.ForEach(path =>
        {
            var assemblyPath = Path.Combine(path, assemblyName);
            if (File.Exists(assemblyPath)) assembly = SysAssembly.LoadFrom(assemblyPath);
        });
        return assembly;
    }

    /// <summary>
    ///     加载配置文件中的程序集路径
    /// </summary>
    public static void LoadAssemblyPath()
    {
        var nodes = ConfigHelper.GetElements("Path");
        foreach (XmlNode node in nodes!)
        {
            var item = new AssemblyPathConfigItem();
            item.GenerateFromXmlNode(node);
            if (item.Path != null)
                _assemblyPaths.Add(item.Path);
        }

        AppDomain.CurrentDomain.AssemblyResolve += OnResolveAssembly;
        Log.Information("Loading Assembly Path from configuration file");
    }

    /// <summary>
    ///     获取所有程序集
    /// </summary>
    /// <returns>
    ///     返回所有程序集
    /// </returns>
    public static List<string> GetAllAssemblies()
    {
        var result = new List<string>();
        _assemblyPaths.ForEach(path =>
        {
            var files = Directory.GetFiles(path, "*.dll");
            // 只存储名称，不记录路径和后缀
            result.AddRange(files.Select(Path.GetFileNameWithoutExtension)!);
        });
        return result;
    }

    /// <summary>
    ///     获取所有包含指定特性的类型
    /// </summary>
    /// <typeparam name="T">
    ///     特性类型
    /// </typeparam>
    /// <returns>
    ///     返回包含指定特性的类型
    /// </returns>
    public static List<Type> GetTypeWithAttribute<T>() where T : Attribute
    {
        var result = new List<Type>();
        _assemblyPaths.ForEach(path =>
        {
            var files = Directory.GetFiles(path, "*.dll");
            foreach (var file in files)
            {
                var assembly = SysAssembly.LoadFrom(file);
                var types = assembly.GetTypes();
                result.AddRange(types.Where(t => t.GetCustomAttribute<T>() != null));
            }
        });
        return result;
    }
}