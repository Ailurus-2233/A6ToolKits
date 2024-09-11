using Microsoft.Extensions.DependencyInjection;

namespace A6ToolKits.MVVM;

/// <summary>
///     基于 Microsoft.Extensions.DependencyInjection 的 IoC 容器封装类
///     可以通过 IoC.Add 方法注册类型到 IoC 容器中, 通过 IoC.Get 方法获取类型的实例
/// </summary>
public static class IoC
{
    private static ServiceCollection? _services;

    private static ServiceProvider ServiceProvider => GetServiceProvider();

    private static ServiceCollection Services => _services ??= [];

    #region Register

    /// <summary>
    ///     注册类型到 IoC 容器中
    /// </summary>
    /// <param name="type">
    ///     一个可以实例化的类型
    /// </param>
    public static void Add(Type type)
    {
        IsLegal(type, null);
        Services.AddTransient(type);
    }

    /// <summary>
    ///     注册类型到 IoC 容器中
    /// </summary>
    /// <param name="serviceType">
    ///     一个接口或抽象类
    /// </param>
    /// <param name="implementationType">
    ///     一个可以实例化的类型
    /// </param>
    public static void Add(Type serviceType, Type implementationType)
    {
        IsLegal(serviceType, implementationType);
        Services.AddTransient(serviceType, implementationType);
    }

    /// <summary>
    ///     注册类型到 IoC 容器中
    /// </summary>
    /// <typeparam name="TService">
    ///     一个可以实例化的类型
    /// </typeparam>
    public static void Add<TService>() where TService : class
    {
        Services.AddTransient<TService>();
    }

    /// <summary>
    ///     注册类型到 IoC 容器中
    /// </summary>
    /// <typeparam name="TService">
    ///     一个接口或抽象类
    /// </typeparam>
    /// <typeparam name="TImplementation">
    ///     一个可以实例化的类型，并且实现了TService
    /// </typeparam>
    public static void Add<TService, TImplementation>()
        where TService : class where TImplementation : class, TService
    {
        Services.AddTransient<TService, TImplementation>();
    }

    /// <summary>
    ///     注册一个单例到 IoC 容器中
    /// </summary>
    /// <param name="type">
    ///     一个可以实例化的类型
    /// </param>
    public static void AddSingleton(Type type)
    {
        IsLegal(type, null);
        Services.AddSingleton(type);
    }

    /// <summary>
    ///     注册一个单例到 IoC 容器中
    /// </summary>
    /// <param name="serviceType">
    ///     一个接口或抽象类
    /// </param>
    /// <param name="implementationType">
    ///     一个可以实例化的类型，并且实现了serviceType
    /// </param>
    public static void AddSingleton(Type serviceType, Type implementationType)
    {
        IsLegal(serviceType, implementationType);
        Services.AddSingleton(serviceType, implementationType);
    }

    /// <summary>
    ///     注册一个具体实例到 IoC 容器中
    /// </summary>
    /// <param name="type">
    ///     可以是类、接口、抽象类
    /// </param>
    /// <param name="instance">
    ///     具体实例，必须是type的实例
    /// </param>
    public static void AddSingleton(Type type, object instance)
    {
        IsLegal(type, instance);
        Services.AddSingleton(type, instance);
    }

    /// <summary>
    ///     注册一个单例到 IoC 容器中
    /// </summary>
    /// <typeparam name="TService">
    ///     一个可以实例化的类型
    /// </typeparam>
    public static void AddSingleton<TService>() where TService : class
    {
        Services.AddSingleton<TService>();
    }

    /// <summary>
    ///     注册一个单例到 IoC 容器中
    /// </summary>
    /// <param name="service">
    ///     一个具体实例
    /// </param>
    /// <typeparam name="TService">
    ///     一个可以实例化的类型
    /// </typeparam>
    public static void AddSingleton<TService>(TService service) where TService : class
    {
        Services.AddSingleton(service);
    }

    /// <summary>
    ///     注册一个单例到 IoC 容器中
    /// </summary>
    /// <typeparam name="TService">
    ///     一个接口或抽象类
    /// </typeparam>
    /// <typeparam name="TImplementation">
    ///     一个可以实例化的类型，并且实现了TService
    /// </typeparam>
    public static void AddSingleton<TService, TImplementation>()
        where TService : class where TImplementation : class, TService
    {
        Services.AddSingleton<TService, TImplementation>();
    }

    /// <summary>
    ///     注册一个具体实例到 IoC 容器中
    /// </summary>
    /// <param name="implementation">
    ///     具体实例，必须是 TImplementation 的实例
    /// </param>
    /// <typeparam name="TService">
    ///     一个可以实例化的类型
    /// </typeparam>
    /// <typeparam name="TImplementation">
    ///     一个可以实例化的类型，并且实现了 TService
    /// </typeparam>
    public static void AddSingleton<TService, TImplementation>(TImplementation implementation)
        where TService : class where TImplementation : class, TService
    {
        Services.AddSingleton<TService>(implementation);
    }

    /// <summary>
    ///     注册一个实例工厂到 IoC 容器中
    /// </summary>
    /// <param name="factory">
    ///     具体的构造实例工厂
    /// </param>
    /// <typeparam name="TService">
    ///     类，接口，抽象类
    /// </typeparam>
    public static void AddByFactory<TService>(Func<IServiceProvider, TService> factory) where TService : class
    {
        Services.AddTransient(factory);
    }

    /// <summary>
    ///     注册一个单例实例工厂到 IoC 容器中
    /// </summary>
    /// <param name="factory">
    ///     具体的构造实例工厂
    /// </param>
    /// <typeparam name="TService">
    ///     类，接口，抽象类
    /// </typeparam>
    public static void AddSingletonByFactory<TService>(Func<IServiceProvider, TService> factory) where TService : class
    {
        Services.AddSingleton(factory);
    }

    #endregion

    #region Get

    /// <summary>
    ///     从 IoC 容器中获取类型的实例
    /// </summary>
    /// <typeparam name="TService">
    ///     需要从 IoC 容器中获取的类型
    /// </typeparam>
    /// <returns>
    ///     返回类型的实例
    /// </returns>
    public static TService Get<TService>() where TService : class
    {
        return ServiceProvider.GetRequiredService<TService>();
    }

    /// <summary>
    ///     尝试从 IoC 容器中获取类型的实例
    /// </summary>
    /// <typeparam name="TService">
    ///     需要从 IoC 容器中获取的类型
    /// </typeparam>
    /// <returns>
    ///     返回类型的实例，如果获取失败则返回 null
    /// </returns>
    public static TService? TryGet<TService>() where TService : class
    {
        return ServiceProvider.GetService<TService>();
    }

    /// <summary>
    ///     从 IoC 容器中获取类型的实例
    /// </summary>
    /// <param name="serviceType">
    ///     需要从 IoC 容器中获取的类型
    /// </param>
    /// <returns>
    ///     返回类型的实例
    /// </returns>
    public static object Get(Type serviceType)
    {
        return ServiceProvider.GetRequiredService(serviceType);
    }

    #endregion

    #region Creator

    /// <summary>
    ///     基于 IoC 中的注册信息创建一个实例
    /// </summary>
    /// <typeparam name="TService">
    ///     需要创建的类型
    /// </typeparam>
    /// <returns>
    ///     返回创建的实例
    /// </returns>
    public static TService Create<TService>() where TService : class
    {
        return ActivatorUtilities.CreateInstance<TService>(ServiceProvider);
    }

    /// <summary>
    ///     基于 IoC 中的注册信息创建一个实例
    /// </summary>
    /// <param name="serviceType">
    ///     需要创建的类型
    /// </param>
    /// <returns>
    ///     返回创建的实例
    /// </returns>
    public static object Create(Type serviceType)
    {
        return ActivatorUtilities.CreateInstance(ServiceProvider, serviceType);
    }

    #endregion

    #region private methods

    /// <summary>
    ///     获取 ServiceProvider 实例
    /// </summary>
    /// <returns></returns>
    private static ServiceProvider GetServiceProvider()
    {
        return Services.BuildServiceProvider();
    }

    /// <summary>
    ///     检查 IoC 注册的类型是否合法
    /// </summary>
    /// <param name="serviceType">
    ///     基类、接口、抽象类
    /// </param>
    /// <param name="implementationType">
    ///     实现类
    /// </param>
    /// <returns>
    ///     如果注册的类型合法则返回 true
    /// </returns>
    /// <exception cref="ArgumentException">
    ///     当注册的类型不合法时抛出异常
    /// </exception>
    private static void IsLegal(Type serviceType, Type? implementationType)
    {
        if (serviceType is { IsInterface: false, IsAbstract: false, IsClass: false })
            throw new ArgumentException("Service type must be a base class or interface or abstract class");

        if (implementationType == null) return;

        if (!implementationType.IsClass || implementationType.IsAbstract)
            throw new ArgumentException("Implementation type must be a non-abstract class");

        if (!serviceType.IsAssignableFrom(implementationType))
            throw new ArgumentException("Implementation type must be assignable to service type");
    }

    /// <summary>
    ///     检查 IoC 注册单例实例时是否合法
    /// </summary>
    /// <param name="type">
    ///     需要注册的类型
    /// </param>
    /// <param name="instance">
    ///     具体实例
    /// </param>
    /// <returns>
    ///     如果注册的类型合法则返回 true
    /// </returns>
    /// <exception cref="ArgumentException">
    ///     当注册的类型不合法时抛出异常
    /// </exception>
    private static void IsLegal(Type type, object instance)
    {
        if (!type.IsInstanceOfType(instance))
            throw new ArgumentException("Instance must be an instance of type");
    }

    #endregion
}