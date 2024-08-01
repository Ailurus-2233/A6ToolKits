using System.Reflection;
using A6ToolKits.Common;
using A6ToolKits.Common.Attributes;
using A6ToolKits.MVVM.Starter;
using Avalonia;
using DryIoc;

namespace A6ToolKits.MVVM.Container;

/// <summary>
/// IoC container
/// </summary>
public static class IoC
{
    private static IContainer? _container;

    public static void Register<TInterface, TImplementation>() where TImplementation : TInterface
    {
        _container.Register<TInterface, TImplementation>();
    }

    public static void Register<T>() where T : class
    {
        _container.Register<T>();
    }

    public static void RegisterInstance<T>(T instance)
    {
        _container.RegisterInstance(instance);
    }

    public static T Get<T>()
    {
        return Resolve<T>();
    }

    public static T GetInstance<T>()
    {
        return Resolve<T>();
    }

    public static T Resolve<T>()
    {
        return _container.Resolve<T>();
    }

    public static object Get(Type type)
    {
        return Resolve(type);
    }

    public static object GetInstance(Type type)
    {
        return Resolve(type);
    }

    public static object Resolve(Type type)
    {
        return _container.Resolve(type);
    }

    public static void SetContainer(IContainerRegistry provider)
    {
        AutoRegisterViewModel(provider);
        _container = provider.GetContainer();
    }

    private static void AutoRegisterViewModel(IContainerRegistry containerRegistry)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var types = assemblies.SelectMany(assembly => assembly.GetTypes())
            .Where(type => type is { IsClass: true, IsAbstract: false } &&
                           type.GetCustomAttribute<AutoRegisterAttribute>() != null)
            .ToArray();

        var modelTypes = types.Where(type => type.Name.EndsWith("Model")).ToDictionary(type => type.Name);
        var notModelTypes = types.Where(type => !type.Name.EndsWith("Model")).ToArray();

        foreach (var type in notModelTypes)
        {
            if (modelTypes.TryGetValue($"{type.Name}Model", out var modelType))
            {
                containerRegistry.RegisterForNavigation(type, modelType.ToString());
                ViewModelLocationProvider.Register(type.ToString(), () => Get(modelType));
            }
            else
                containerRegistry.Register(type);
        }
    }
}