using System;

namespace A6ToolKits.Instance;

/// <summary>
///     IoC 容器接口，包含实例的注册和获取
/// </summary>
public interface IIoCContainer : ICreator
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

    // 注册实例
    void Register(Type serviceType);
    void Register(Type serviceType, Type implementationType);
    void Register<TService>() where TService : class;
    void Register<TService, TImplementation>() where TImplementation : class, TService where TService : class;
    void Register<TService>(Func<IServiceProvider, TService> implementationFactory) where TService : class;
    void RegisterSingleton(Type serviceType);
    void RegisterSingleton(Type serviceType, Type implementationType);
    void RegisterSingleton(Type serviceType, object serviceInstance);
    void RegisterSingleton<TService, TImplementation>() where TImplementation : class, TService where TService : class;
    void RegisterSingleton<TService>(TService instance) where TService : class;
    void RegisterSingleton<TService>(Func<IServiceProvider, TService> implementationFactory) where TService : class;

    // 获取实例
    TService? GetInstance<TService>();
    object? GetInstance(Type serviceType);

#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释
}