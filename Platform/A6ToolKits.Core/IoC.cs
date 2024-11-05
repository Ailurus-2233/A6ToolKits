using System;
using A6ToolKits.Instance;

namespace A6ToolKits;

/// <summary>
///     IoC 容器，用于实例的注册和获取
/// </summary>
public static class IoC
{
    private static readonly IIoCContainer container = IoCContainer.Instance;

    /// <summary>
    ///     创建指定类型的实例
    /// </summary>
    /// <typeparam name="TService">
    ///     类型
    /// </typeparam>
    /// <returns>
    ///     返回类型的实例，如果创建失败则返回 null
    /// </returns>
    public static TService? Create<TService>() where TService : class
    {
        return container.Create<TService>();
    }

    /// <summary>
    ///     创建指定类型的实例
    /// </summary>
    /// <param name="type">
    ///     类型
    /// </param>
    /// <returns>
    ///     返回类型的实例，如果创建失败则返回 null
    /// </returns>
    public static object? Create(Type type)
    {
        return container.Create(type);
    }

    /// <summary>
    ///     以 Transient 模式注册服务
    /// </summary>
    /// <param name="serviceType">
    ///     服务类型
    /// </param>
    public static void Register(Type serviceType)
    {
        container.Register(serviceType);
    }

    /// <summary>
    ///     以 Transient 模式注册服务
    /// </summary>
    /// <param name="serviceType">
    ///     服务类型
    /// </param>
    /// <param name="implementationType">
    ///     服务实现类型
    /// </param>
    public static void Register(Type serviceType, Type implementationType)
    {
        container.Register(serviceType, implementationType);
    }

    /// <summary>
    ///     以 Transient 模式注册服务
    /// </summary>
    /// <typeparam name="TService">
    ///     服务类型，是一个具体的类
    /// </typeparam>
    public static void Register<TService>() where TService : class
    {
        container.Register<TService>();
    }

    /// <summary>
    ///     以 Transient 模式注册服务
    /// </summary>
    /// <typeparam name="TService">
    ///     服务类型，是一个接口
    /// </typeparam>
    /// <typeparam name="TImplementation">
    ///     服务实现类型，是一个具体的类
    /// </typeparam>
    public static void Register<TService, TImplementation>()
        where TImplementation : class, TService where TService : class
    {
        container.Register<TService, TImplementation>();
    }

    /// <summary>
    ///     以 Transient 模式注册服务，并以工厂方法创建服务实例
    /// </summary>
    /// <param name="implementationFactory">
    ///     服务实现工厂方法，接受一个 IServiceProvider 参数，返回一个服务实例
    /// </param>
    /// <typeparam name="TService">
    ///     服务类型，是一个接口或者具体的类
    /// </typeparam>
    public static void Register<TService>(Func<IServiceProvider, TService> implementationFactory) where TService : class
    {
        container.Register(implementationFactory);
    }

    /// <summary>
    ///     以 Singleton 模式注册服务
    /// </summary>
    /// <param name="serviceType">
    ///     服务类型
    /// </param>
    public static void RegisterSingleton(Type serviceType)
    {
        container.RegisterSingleton(serviceType);
    }

    /// <summary>
    ///     以 Singleton 模式注册服务
    /// </summary>
    /// <param name="serviceType">
    ///     服务类型
    /// </param>
    /// <param name="implementationType">
    ///     服务实现类型
    /// </param>
    public static void RegisterSingleton(Type serviceType, Type implementationType)
    {
        container.RegisterSingleton(serviceType, implementationType);
    }

    /// <summary>
    ///     以 Singleton 模式注册服务
    /// </summary>
    /// <param name="serviceType">
    ///     服务类型
    /// </param>
    /// <param name="serviceInstance">
    ///     服务实例
    /// </param>
    public static void RegisterSingleton(Type serviceType, object serviceInstance)
    {
        container.RegisterSingleton(serviceType, serviceInstance);
    }

    /// <summary>
    ///     以 Singleton 模式注册服务
    /// </summary>
    /// <typeparam name="TService">
    ///     服务类型，是一个接口
    /// </typeparam>
    /// 服务实现类型，是一个具体的类
    /// <typeparam name="TImplementation">
    /// </typeparam>
    public static void RegisterSingleton<TService, TImplementation>()
        where TImplementation : class, TService where TService : class
    {
        container.RegisterSingleton<TService, TImplementation>();
    }

    /// <summary>
    ///     以 Singleton 模式注册服务
    /// </summary>
    /// <param name="service">
    ///     服务实例
    /// </param>
    /// <typeparam name="TService">
    ///     服务类型，是一个接口或者具体的类
    /// </typeparam>
    public static void RegisterSingleton<TService>(TService service) where TService : class
    {
        container.RegisterSingleton(service);
    }

    /// <summary>
    ///     以 Singleton 模式注册服务，并以工厂方法创建服务实例
    /// </summary>
    /// <param name="implementationFactory"></param>
    /// <typeparam name="TService"></typeparam>
    /// <exception cref="NotImplementedException"></exception>
    public static void RegisterSingleton<TService>(Func<IServiceProvider, TService> implementationFactory)
        where TService : class
    {
        container.RegisterSingleton(implementationFactory);
    }

    /// <summary>
    ///     获取服务实例
    /// </summary>
    /// <typeparam name="TService">
    ///     服务类型
    /// </typeparam>
    /// <returns>
    ///     返回服务实例
    /// </returns>
    public static TService? GetInstance<TService>()
    {
        return container.GetInstance<TService>();
    }

    /// <summary>
    ///     获取服务实例
    /// </summary>
    /// <param name="serviceType">
    ///     服务类型
    /// </param>
    /// <returns>
    ///     返回服务实例
    /// </returns>
    public static object? GetInstance(Type serviceType)
    {
        return container.GetInstance(serviceType);
    }
}