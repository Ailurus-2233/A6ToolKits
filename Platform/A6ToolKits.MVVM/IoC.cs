namespace A6ToolKits.MVVM;

/// <summary>
///     静态IoC类，提供了一些通用的方法给外部
/// </summary>
public static class IoC
{
    #region Register

    /// <summary>
    ///     注册类型到 IoC 容器中
    /// </summary>
    /// <param name="type">
    ///     一个可以实例化的类型
    /// </param>
    public static void Add(Type type)
    {
        MVVMCreator.Instance.Add(type);
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
        MVVMCreator.Instance.Add(serviceType, implementationType);
    }

    /// <summary>
    ///     注册类型到 IoC 容器中
    /// </summary>
    /// <typeparam name="TService">
    ///     一个可以实例化的类型
    /// </typeparam>
    public static void Add<TService>() where TService : class
    {
        MVVMCreator.Instance.Add(typeof(TService));
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
        MVVMCreator.Instance.Add(typeof(TService), typeof(TImplementation));
    }

    /// <summary>
    ///     注册一个单例到 IoC 容器中
    /// </summary>
    /// <param name="type">
    ///     一个可以实例化的类型
    /// </param>
    public static void AddSingleton(Type type)
    {
        MVVMCreator.Instance.AddSingleton(type);
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
        MVVMCreator.Instance.AddSingleton(serviceType, implementationType);
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
    public static void AddSingleton(Type type, object obj)
    {
        MVVMCreator.Instance.AddSingleton(type, obj);
    }

    /// <summary>
    ///     注册一个单例到 IoC 容器中
    /// </summary>
    /// <typeparam name="TService">
    ///     一个可以实例化的类型
    /// </typeparam>
    public static void AddSingleton<TService>() where TService : class
    {
        MVVMCreator.Instance.AddSingleton(typeof(TService));
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
    public static void AddSingleton<TService>(TService obj) where TService : class
    {
        MVVMCreator.Instance.AddSingleton(typeof(TService), obj);
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
        MVVMCreator.Instance.AddSingleton(typeof(TService), typeof(TImplementation));
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
        MVVMCreator.Instance.AddSingleton(typeof(TService), implementation);
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
        MVVMCreator.Instance.AddByFactory<TService>(factory);
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
        MVVMCreator.Instance.AddByFactory<TService>(factory);
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
        return MVVMCreator.Instance.Get<TService>();
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
    public static bool TryGet<TService>(out TService? obj) where TService : class
    {
        return MVVMCreator.Instance.TryGet(out obj);
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
        return MVVMCreator.Instance.Get(serviceType);
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
    public static bool TryGet(Type type, out object? obj)
    {
        return MVVMCreator.Instance.TryGet(type, out obj);
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
    public static object? Create(string type, string? assembly = null)
    {
        return MVVMCreator.Instance.Create(type, assembly);
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
    public static object? GetOrCreate(string type, string? assembly = null)
    {
        return MVVMCreator.Instance.GetOrCreate(type, assembly);
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
    public static TService Create<TService>() where TService : class
    {
        return MVVMCreator.Instance.Create<TService>();
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
    public static TService? GetOrCreate<TService>() where TService : class
    {
        return MVVMCreator.Instance.GetOrCreate<TService>();
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
    public static object Create(Type type)
    {
        return MVVMCreator.Instance.Create(type);
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
    public static object? GetOrCreate(Type type)
    {
        return MVVMCreator.Instance.GetOrCreate(type);
    }

    #endregion
}