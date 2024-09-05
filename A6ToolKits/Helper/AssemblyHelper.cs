using System;
using System.Linq;

namespace A6ToolKits.Helper;

public static class AssemblyHelper
{
    public static Type? LoadType(string assemblyName, string typeName)
    {
        var assembly = System.Reflection.Assembly.LoadFrom(assemblyName);
        var type = assembly.GetTypes().FirstOrDefault(t => t.FullName == typeName);
        return type;
    }

    public static T? CreateInstance<T>(string assemblyName, string typeName) 
        where T : class
    {
        var type = LoadType(assemblyName, typeName);
        if (type == null)
        {
            return null;
        }

        return (T)Activator.CreateInstance(type)!;
    }
    
    public static object? CreateInstance(string assemblyName, string typeName) 
    {
        var type = LoadType(assemblyName, typeName);
        return type == null ? null : Activator.CreateInstance(type)!;
    }
}