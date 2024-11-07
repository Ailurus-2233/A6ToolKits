using System.Reflection;
using A6ToolKits.Common.Attributes;
using A6ToolKits.Helper.AssemblyManager;
using Avalonia.Controls;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace A6ToolKits.Container;

/// <summary>
///     自动注册帮助类
/// </summary>
public static class AutomaticRegister
{
    /// <summary>
    ///     从所有加载的程序集中查找带有 AutoRegisterAttribute 特性的类型并注册到 IoC 容器中
    ///     注意：最好不要使用，因为会加载所有的程序集
    /// </summary>
    public static void AutoRegisterAll()
    {
        var assembly = Assembly.GetEntryAssembly();
        if (assembly == null) return;
        AutoRegister(assembly);

        var assemblies = LoadHelper.GetAllAssemblies();
        foreach (var loadedAssembly in assemblies.Select(Assembly.Load)) AutoRegister(loadedAssembly);
    }

    /// <summary>
    ///     从程序集中查找带有 AutoRegisterAttribute 特性的类型并注册到 IoC 容器中
    /// </summary>
    /// <param name="assembly">
    ///     程序集
    /// </param>
    public static void AutoRegister(Assembly assembly)
    {
        var serviceTypes =
            assembly.GetTypes().Where(type => type.GetCustomAttributes<AutoRegisterAttribute>().Any());
        RegisterServices(serviceTypes);

        var viewModelTypes =
            assembly.GetTypes().Where(type => type.GetCustomAttributes<TargetViewAttribute>().Any());
        RegisterViewModel(viewModelTypes);
    }

    /// <summary>
    ///     注册类型到 IoC 容器中
    /// </summary>
    /// <param name="types">
    ///     需要要注册的类型列表
    /// </param>
    public static void RegisterServices(IEnumerable<Type> types)
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

    /// <summary>
    ///     注册 ViewModel 到 IoC 容器中
    /// </summary>
    /// <param name="types">
    ///     需要要注册的 ViewModel 类型列表
    /// </param>
    public static void RegisterViewModel(IEnumerable<Type> types)
    {
        foreach (var type in types)
        {
            var attribute = type.GetCustomAttribute<TargetViewAttribute>();
            if (attribute == null) continue;

            var targetViewType = attribute.ViewType;

            switch (attribute.RegisterType)
            {
                case RegisterType.Singleton:
                    if (IoC.Create(targetViewType) is not UserControl targetView) continue;
                    var viewModel = IoC.Create(type);
                    if (viewModel == null) continue;
                    targetView.DataContext = viewModel;
                    IoC.RegisterSingleton(type, viewModel);
                    IoC.RegisterSingleton(targetViewType, targetView);
                    break;
                case RegisterType.Transient:
                default:
                    IoC.Register(targetViewType);
                    IoC.Register(type);
                    break;
            }
        }
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
            if (property.GetValue(target) == null)
                property.SetValue(target, service);
        }
    }
}