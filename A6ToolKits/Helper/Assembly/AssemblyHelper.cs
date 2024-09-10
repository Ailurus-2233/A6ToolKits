using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using A6ToolKits.Helper.Config;
using SysAssembly = System.Reflection.Assembly;

namespace A6ToolKits.Helper.Assembly;

public static class AssemblyHelper
{
    private static List<string> AssemblyPaths { get; set; } = [];

    private static Type? LoadType(string? assemblyName, string typeName)
    {
        if (assemblyName == null) return Type.GetType(typeName);
        SysAssembly? assembly = null;
        AssemblyPaths.ForEach(path =>
        {
            var assemblyPath = Path.Combine(path, assemblyName);
            if (File.Exists(assemblyPath))
            {
                assembly = SysAssembly.LoadFrom(assemblyPath);
            }
        });
        var type = assembly?.GetTypes().FirstOrDefault(t => t.FullName == typeName);
        return type;
    }

    public static T? CreateInstance<T>(string? assemblyName, string typeName)
        where T : class
    {
        var type = LoadType(assemblyName, typeName);
        return type == null ? null : Activator.CreateInstance(type)! as T;
    }

    public static object? CreateInstance(string? assemblyName, string typeName)
    {
        var type = LoadType(assemblyName, typeName);
        return type == null ? null : Activator.CreateInstance(type)!;
    }

    public static SysAssembly? OnResolveAssembly(object? sender, ResolveEventArgs args)
    {
        var assemblyName = $"{new AssemblyName(args.Name).Name}.dll";
        SysAssembly? assembly = null;
        AssemblyPaths.ForEach(path =>
        {
            var assemblyPath = Path.Combine(path, assemblyName);
            if (File.Exists(assemblyPath))
            {
                assembly = SysAssembly.LoadFrom(assemblyPath);
            }
        });
        return assembly;
    }

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