using System.Reflection;
using A6ToolKits.Attributes;
using A6ToolKits.Helpers;
using Avalonia.Controls;

// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace A6ToolKits.Container;

/// <summary>
///     自动注册帮助类
/// </summary>
public static class ViewModelAutomaticRegister
{
    /// <summary>
    ///     从所有加载的程序集中查找带有 AutoRegisterAttribute 特性的类型并注册到 IoC 容器中
    ///     注意：最好不要使用，因为会加载所有的程序集
    /// </summary>
    public static void AutoRegister()
    {
        var assembly = Assembly.GetEntryAssembly();
        if (assembly == null) return;
        AutoRegister(assembly);

        var assemblies = AssemblyHelper.GetAllAssemblies();
        foreach (var loadedAssembly in assemblies.Select(Assembly.Load)) 
            AutoRegister(loadedAssembly);
    }

    /// <summary>
    ///     从程序集中查找带有 AutoRegisterAttribute 特性的类型并注册到 IoC 容器中
    /// </summary>
    /// <param name="assembly">
    ///     程序集
    /// </param>
    public static void AutoRegister(Assembly assembly)
    {
        var viewModelTypes =
            assembly.GetTypes().Where(type => type.GetCustomAttributes<TargetViewAttribute>().Any());
        RegisterViewModel(viewModelTypes);
    }

    /// <summary>
    ///     注册 ViewModel 到 IoC 容器中
    /// </summary>
    /// <param name="types">
    ///     需要要注册的 ViewModel 类型列表
    /// </param>
    private static void RegisterViewModel(IEnumerable<Type> types)
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
                    AutomaticRegister.InjectDependencies(viewModel);
                    AutomaticRegister.InjectDependencies(targetView);
                    break;
                case RegisterType.Transient:
                default:
                    IoC.Register(targetViewType);
                    IoC.Register(type);
                    break;
            }
        }
    }
}