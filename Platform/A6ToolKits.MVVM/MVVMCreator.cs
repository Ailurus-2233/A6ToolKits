using A6ToolKits.Assembly;
using A6ToolKits.Instance;
using Microsoft.Extensions.DependencyInjection;

namespace A6ToolKits.MVVM;

/// <summary>
///     基于 Microsoft.Extensions.DependencyInjection 的 IoC 容器封装类
///     可以通过 IoC.Add 方法注册类型到 IoC 容器中, 通过 IoC.Get 方法获取类型的实例
/// </summary>
internal sealed class MVVMCreator : IInstanceCreator
{
    private readonly ServiceCollection _service = [];
    private static readonly Lazy<MVVMCreator> instance = new(() => new MVVMCreator());

    private MVVMCreator()
    { }

    /// <summary>
    /// </summary>
    public static MVVMCreator Instance => instance.Value;
    
    #region Register

    /// <summary>
    ///     注册类型到 IoC 容器中
    /// </summary>
    /// <param name="type">
    ///     一个可以实例化的类型
    /// </param>
    public void Add(Type type)
    {
        Instance.IsLegal(type, null);
        _service.AddTransient(type);
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
    public void Add(Type serviceType, Type implementationType)
    {
        IsLegal(serviceType, implementationType);
        _service.AddTransient(serviceType, implementationType);
    }

    /// <summary>
    ///     注册类型到 IoC 容器中
    /// </summary>
    /// <typeparam name="TService">
    ///     一个可以实例化的类型
    /// </typeparam>
    public void Add<TService>() where TService : class
    {
        _service.AddTransient<TService>();
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
    public void Add<TService, TImplementation>()
        where TService : class where TImplementation : class, TService
    {
        _service.AddTransient<TService, TImplementation>();
    }

    /// <summary>
    ///     注册一个单例到 IoC 容器中
    /// </summary>
    /// <param name="type">
    ///     一个可以实例化的类型
    /// </param>
    public void AddSingleton(Type type)
    {
        IsLegal(type, null);
        _service.AddSingleton(type);
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
    public void AddSingleton(Type serviceType, Type implementationType)
    {
        IsLegal(serviceType, implementationType);
        _service.AddSingleton(serviceType, implementationType);
    }

    /// <summary>
    ///     注册一个具体实例到 IoC 容器中
    /// </summary>
    /// <param name="type">
    ///     可以是类、接口、抽象类
    /// </param>
    /// <param name="obj">
    ///     具体实例，必须是type的实例
    /// </param>
    public void AddSingleton(Type type, object obj)
    {
        IsLegal(type, obj);
        _service.AddSingleton(type, obj);
    }

    /// <summary>
    ///     注册一个单例到 IoC 容器中
    /// </summary>
    /// <typeparam name="TService">
    ///     一个可以实例化的类型
    /// </typeparam>
    public void AddSingleton<TService>() where TService : class
    {
        _service.AddSingleton<TService>();
    }

    /// <summary>
    ///     注册一个单例到 IoC 容器中
    /// </summary>
    /// <param name="obj">
    ///     一个具体实例
    /// </param>
    /// <typeparam name="TService">
    ///     一个可以实例化的类型
    /// </typeparam>
    public void AddSingleton<TService>(TService obj) where TService : class
    {
        _service.AddSingleton(obj);
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
    public void AddSingleton<TService, TImplementation>()
        where TService : class where TImplementation : class, TService
    {
        _service.AddSingleton<TService, TImplementation>();
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
    public void AddSingleton<TService, TImplementation>(TImplementation implementation)
        where TService : class where TImplementation : class, TService
    {
        _service.AddSingleton<TService>(implementation);
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
    public void AddByFactory<TService>(Func<IServiceProvider, TService> factory) where TService : class
    {
        _service.AddTransient(factory);
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
    public void AddSingletonByFactory<TService>(Func<IServiceProvider, TService> factory) where TService : class
    {
        _service.AddSingleton(factory);
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
    public TService Get<TService>() where TService : class
    {
        var provider = _service.BuildServiceProvider();
        return provider.GetRequiredService<TService>();
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
    public bool TryGet<TService>(out TService? obj) where TService : class
    {
        var provider = _service.BuildServiceProvider();
        obj = provider.GetService<TService>();
        return obj != null;
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
    public object Get(Type serviceType)
    {
        var provider = _service.BuildServiceProvider();
        return provider.GetRequiredService(serviceType);
    }

    /// <summary>
    ///     尝试从 IoC 容器中获取类型的实例
    /// </summary>
    /// <param name="type">
    ///     需要从 IoC 容器中获取的类型
    /// </param>
    /// <param name="obj">
    ///     返回类型的实例
    /// </param>
    /// <returns>
    ///     如果获取成功则返回 true
    /// </returns>
    public bool TryGet(Type type, out object? obj)
    {
        var provider = _service.BuildServiceProvider();
        obj = provider.GetService(type);
        return obj != null;
    }

    #endregion

    #region Creator

    /// <summary>
    ///     创建指定程序集类型的实例
    /// </summary>
    /// <param name="type">
    ///     类型名称
    /// </param>
    /// <param name="assembly">
    ///     类型所在的程序集名称，如果为 null 则从当前程序集中查找
    /// </param>
    /// <returns>
    ///     返回类型的实例，如果创建失败则返回 null
    /// </returns>
    public object? Create(string type, string? assembly = null)
    {
        var target = assembly == null ? Type.GetType(type) : AssemblyHelper.LoadType(type, assembly);
        return target == null ? null : this.Create(target);
    }

    /// <summary>
    ///     从 IoC 容器获取指定类型的实例，如果不存在则创建一个新的实例
    /// </summary>
    /// <param name="type">
    ///     类型名称
    /// </param>
    /// <param name="assembly">
    ///     类型所在的程序集名称，如果为 null 则从当前程序集中查找
    /// </param>
    /// <returns>
    ///     返回类型的实例，如果创建失败则返回 null
    /// </returns>
    public object? GetOrCreate(string type, string? assembly = null)
    {
        var targetType = assembly == null ? Type.GetType(type) : AssemblyHelper.LoadType(type, assembly);
        return targetType == null ? null : GetOrCreate(targetType);
    }


    /// <summary>
    ///     基于 IoC 中的注册信息创建一个实例
    /// </summary>
    /// <typeparam name="TService">
    ///     需要创建的类型
    /// </typeparam>
    /// <returns>
    ///     返回创建的实例
    /// </returns>
    public TService Create<TService>() where TService : class
    {
        var provider = _service.BuildServiceProvider();
        return ActivatorUtilities.CreateInstance<TService>(provider);
    }

    /// <summary>
    ///     从 IoC 容器获取指定类型的实例，如果不存在则创建一个新的实例
    /// </summary>
    /// <typeparam name="TService">
    ///     类型
    /// </typeparam>
    /// <returns>
    ///     返回类型的实例，如果创建失败则返回 null
    /// </returns>
    public TService? GetOrCreate<TService>() where TService : class
    {
        return TryGet<TService>(out var result) ? result : this.Create<TService>();
    }

    /// <summary>
    ///     基于 IoC 中的注册信息创建一个实例
    /// </summary>
    /// <param name="type">
    ///     需要创建的类型
    /// </param>
    /// <returns>
    ///     返回创建的实例
    /// </returns>
    public object Create(Type type)
    {
        var provider = _service.BuildServiceProvider();
        return ActivatorUtilities.CreateInstance(provider, type);
    }

    /// <summary>
    ///     从 IoC 容器获取指定类型的实例，如果不存在则创建一个新的实例
    /// </summary>
    /// <param name="type">
    ///     指定类型
    /// </param>
    /// <returns>
    ///     返回类型的实例，如果创建失败则返回 null
    /// </returns>
    public object? GetOrCreate(Type type)
    {
        return TryGet(type, out var result) ? result : this.Create(type);
    }

    #endregion

    #region private methods

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
    private void IsLegal(Type serviceType, Type? implementationType)
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
    /// <param name="obj">
    ///     具体实例
    /// </param>
    /// <returns>
    ///     如果注册的类型合法则返回 true
    /// </returns>
    /// <exception cref="ArgumentException">
    ///     当注册的类型不合法时抛出异常
    /// </exception>
    private void IsLegal(Type type, object obj)
    {
        if (!type.IsInstanceOfType(obj))
            throw new ArgumentException("Instance must be an instance of type");
    }

    #endregion
}