using System.Reflection;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace A6ToolKits.Helpers;

/// <summary>
///     加载帮助类
/// </summary>
public static class AssemblyHelper
{
    /// <summary>
    ///     程序集路径
    /// </summary>
    public static List<string> AssemblyPaths { get; set; } = ["./"];

    /// <summary>
    ///     接管 AppDomain 解析程序集
    /// </summary>
    public static void ResolveAssembly()
    {
        AppDomain.CurrentDomain.AssemblyResolve += (_, args) =>
        {
            var assemblyName = $"{new AssemblyName(args.Name).Name}.dll";
            System.Reflection.Assembly? assembly = null;
            AssemblyPaths.ForEach(path =>
            {
                var assemblyPath = Path.Combine(path, assemblyName);
                if (File.Exists(assemblyPath)) assembly = System.Reflection.Assembly.LoadFrom(assemblyPath);
            });
            return assembly;
        };
    }

    /// <summary>
    ///     根据程序集名称和类型名称加载类型
    /// </summary>
    /// <param name="type">
    ///     类型名称
    /// </param>
    /// <param name="assemblyName">
    ///     程序集名称
    /// </param>
    /// <returns>
    ///     返回加载的类型
    /// </returns>
    public static Type? LoadType(string type, string? assemblyName = null)
    {
        if (assemblyName == null) return Type.GetType(type);
        System.Reflection.Assembly? assembly = null;
        AssemblyPaths.ForEach(path =>
        {
            var assemblyPath = Path.Combine(path, assemblyName);
            if (File.Exists(assemblyPath)) assembly = System.Reflection.Assembly.LoadFrom(assemblyPath);
        });
        var result = assembly?.GetTypes().FirstOrDefault(t => t.FullName == type);
        return result;
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
        AssemblyPaths.ForEach(path =>
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
        AssemblyPaths.ForEach(path =>
        {
            var files = Directory.GetFiles(path, "*.dll");
            foreach (var file in files)
            {
                var assembly = System.Reflection.Assembly.LoadFrom(file);
                var types = assembly.GetTypes();
                result.AddRange(types.Where(t => t.GetCustomAttribute<T>() != null));
            }
        });
        return result;
    }
}