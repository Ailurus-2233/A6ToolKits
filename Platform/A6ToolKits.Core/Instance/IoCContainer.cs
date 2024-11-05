using System;
using Microsoft.Extensions.DependencyInjection;

namespace A6ToolKits.Instance;

/// <summary>
///     IoC 容器类，用于实现依赖注入
/// </summary>
public class IoCContainer : IIoCContainer
{
    private static readonly Lazy<IoCContainer> instance = new(() => new IoCContainer());
    private readonly IServiceCollection _services;
    private IServiceProvider? _serviceProvider;

    private IoCContainer()
    {
        _services = new ServiceCollection();
    }

    /// <summary>
    ///     获取 IoC 容器实例
    /// </summary>
    public static IoCContainer Instance => instance.Value;

    /// <summary>
    ///     创建指定类型的实例
    /// </summary>
    /// <typeparam name="TService">
    ///     类型
    /// </typeparam>
    /// <returns>
    ///     返回类型的实例，如果创建失败则返回 null
    /// </returns>
    public TService? Create<TService>() where TService : class
    {
        _serviceProvider = _services.BuildServiceProvider();
        return ActivatorUtilities.CreateInstance<TService>(_serviceProvider);
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
    public object? Create(Type type)
    {
        _serviceProvider = _services.BuildServiceProvider();
        return ActivatorUtilities.CreateInstance(_serviceProvider, type);
    }

    /// <summary>
    ///     以 Transient 模式注册服务
    /// </summary>
    /// <param name="serviceType">
    ///     服务类型
    /// </param>
    public void Register(Type serviceType)
    {
        _services.AddTransient(serviceType);
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
    public void Register(Type serviceType, Type implementationType)
    {
        _services.AddTransient(serviceType, implementationType);
    }

    /// <summary>
    ///     以 Transient 模式注册服务
    /// </summary>
    /// <typeparam name="TService">
    ///     服务类型，是一个具体的类
    /// </typeparam>
    public void Register<TService>() where TService : class
    {
        _services.AddTransient<TService>();
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
    public void Register<TService, TImplementation>() where TImplementation : class, TService where TService : class
    {
        _services.AddTransient<TService, TImplementation>();
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
    public void Register<TService>(Func<IServiceProvider, TService> implementationFactory) where TService : class
    {
        _services.AddTransient(implementationFactory);
    }

    /// <summary>
    ///     以 Singleton 模式注册服务
    /// </summary>
    /// <param name="serviceType">
    ///     服务类型
    /// </param>
    public void RegisterSingleton(Type serviceType)
    {
        _services.AddSingleton(serviceType);
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
    public void RegisterSingleton(Type serviceType, Type implementationType)
    {
        _services.AddSingleton(serviceType, implementationType);
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
    public void RegisterSingleton(Type serviceType, object serviceInstance)
    {
        _services.AddSingleton(serviceType, serviceInstance);
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
    public void RegisterSingleton<TService, TImplementation>()
        where TImplementation : class, TService where TService : class
    {
        _services.AddSingleton<TService, TImplementation>();
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
    public void RegisterSingleton<TService>(TService service) where TService : class
    {
        _services.AddSingleton(service);
    }

    /// <summary>
    ///     以 Singleton 模式注册服务，并以工厂方法创建服务实例
    /// </summary>
    /// <param name="implementationFactory"></param>
    /// <typeparam name="TService"></typeparam>
    /// <exception cref="NotImplementedException"></exception>
    public void RegisterSingleton<TService>(Func<IServiceProvider, TService> implementationFactory)
        where TService : class
    {
        _services.AddSingleton(implementationFactory);
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
    public TService? GetInstance<TService>()
    {
        _serviceProvider = _services.BuildServiceProvider();
        return _serviceProvider.GetService<TService>();
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
    public object? GetInstance(Type serviceType)
    {
        _serviceProvider = _services.BuildServiceProvider();
        return _serviceProvider.GetService(serviceType);
    }
}