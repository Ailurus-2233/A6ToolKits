using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using A6ToolKits.Helper.Config;
using SysAssembly = System.Reflection.Assembly;

namespace A6ToolKits.Helper.Assembly;

/// <summary>
///     程序集帮助类
/// </summary>
public static class AssemblyHelper
{
    private static List<string> AssemblyPaths { get; } = [];

    private static Type? LoadType(string? assemblyName, string typeName)
    {
        if (assemblyName == null) return Type.GetType(typeName);
        SysAssembly? assembly = null;
        AssemblyPaths.ForEach(path =>
        {
            var assemblyPath = Path.Combine(path, assemblyName);
            if (File.Exists(assemblyPath)) assembly = SysAssembly.LoadFrom(assemblyPath);
        });
        var type = assembly?.GetTypes().FirstOrDefault(t => t.FullName == typeName);
        return type;
    }

    /// <summary>
    ///     创建指定类型的实例
    /// </summary>
    /// <param name="assemblyName">
    ///     类型所在的程序集名称，如果为 null 则从当前程序集中查找
    /// </param>
    /// <param name="typeName">
    ///     类型名称
    /// </param>
    /// <typeparam name="T">
    ///     类型
    /// </typeparam>
    /// <returns>
    ///     返回类型的实例，如果创建失败则返回 null
    /// </returns>
    public static T? CreateInstance<T>(string? assemblyName, string typeName)
        where T : class
    {
        var type = LoadType(assemblyName, typeName);
        return type == null ? null : Activator.CreateInstance(type)! as T;
    }

    /// <summary>
    ///     创建指定类型的实例
    /// </summary>
    /// <param name="assemblyName">
    ///     类型所在的程序集名称，如果为 null 则从当前程序集中查找
    /// </param>
    /// <param name="typeName">
    ///     类型名称
    /// </param>
    /// <returns>
    ///     返回类型的实例，如果创建失败则返回 null
    /// </returns>
    public static object? CreateInstance(string? assemblyName, string typeName)
    {
        var type = LoadType(assemblyName, typeName);
        return type == null ? null : Activator.CreateInstance(type)!;
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
        AssemblyPaths.ForEach(path =>
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
            var item = new AssemblyConfigItem();
            item.GenerateFromXmlNode(node);
            if (item.Path != null)
                AssemblyPaths.Add(item.Path);
        }
    }
}