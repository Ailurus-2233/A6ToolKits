using System;
using System.Linq;
using System.Reflection;

namespace A6ToolKits.Helper;

public static class AssemblyHelper
{
    private static Type? LoadType(string? assemblyName, string typeName)
    {
        if (assemblyName == null) return Type.GetType(typeName);
        var assembly = Assembly.LoadFrom(assemblyName);
        var type = assembly.GetTypes().FirstOrDefault(t => t.FullName == typeName);
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
}