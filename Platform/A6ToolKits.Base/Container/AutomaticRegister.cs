using System.Reflection;
using A6ToolKits.Attributes;
using A6ToolKits.Helpers;
namespace A6ToolKits.Container;

public static class AutomaticRegister
{
    /// <summary>
    ///     从所有加载的程序集中查找带有 AutoRegisterAttribute 特性的类型并注册到 IoC 容器中
    /// </summary>
    public static void AutoRegister()
    {
        var assembly = Assembly.GetEntryAssembly();
        if (assembly == null) return;
        AutoRegisterService(assembly);

        var assemblies = AssemblyHelper.GetAllAssemblies();
        foreach (var loadedAssembly in assemblies.Select(Assembly.Load)) 
            AutoRegisterService(loadedAssembly);
    }

    public static void AutoRegisterService(Assembly assembly)
    {
        var serviceTypes =
            assembly.GetTypes().Where(type => type.GetCustomAttributes<AutoRegisterAttribute>().Any());
        RegisterServices(serviceTypes);
    }
    
    /// <summary>
    ///     依赖注入，从 IoC 容器中获取依赖并注入到目标对象中
    /// </summary>
    /// <param name="target">
    ///     要注入依赖的对象
    /// </param>
    public static void InjectDependencies(object? target)
    {
        if (target == null) return;

        var properties = target.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (var property in properties)
        {
            if (!property.IsDefined(typeof(InjectAttribute), false)) continue;
            var service = IoC.GetInstance(property.PropertyType);
            if (service == null)
                throw new InvalidOperationException($"Cannot inject service for property {property.Name}");
            property.SetValue(target, service);
        }
    }
    
    /// <summary>
    ///     注册类型到 IoC 容器中
    /// </summary>
    /// <param name="types">
    ///     需要要注册的类型列表
    /// </param>
    private static void RegisterServices(IEnumerable<Type> types)
    {
        foreach (var type in types)
        {
            var attribute = type.GetCustomAttribute<AutoRegisterAttribute>();
            if (attribute == null) continue;

            var interfaceType = attribute.InterfaceType ?? type;
            var registerType = attribute.RegisterType;

            switch (registerType)
            {
                case RegisterType.Singleton:
                    var instance = IoC.Create(type);
                    if (instance == null) continue;
                    InjectDependencies(instance);
                    IoC.RegisterSingleton(interfaceType, instance);
                    break;
                case RegisterType.Transient:
                default:
                    IoC.Register(interfaceType, type);
                    break;
            }
        }
    }
}