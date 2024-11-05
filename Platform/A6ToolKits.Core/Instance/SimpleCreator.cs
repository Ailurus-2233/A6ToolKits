using System;

namespace A6ToolKits.Instance;

/// <summary>
///     基本的实例构造器，用于运行时创建实例对象，但只能用于无参构造函数的实例创建。
///     该类只用于 Bootstrapper 初始化过程中，如有特殊需求请自行实现 IInstanceCreator 接口。
/// </summary>
public sealed class SimpleCreator : ICreator
{
    /// <summary>
    ///     创建指定类型的实例
    /// </summary>
    /// <typeparam name="T">
    ///     类型
    /// </typeparam>
    /// <returns>
    ///     返回类型的实例，如果创建失败则返回 null
    /// </returns>
    public T? Create<T>() where T : class
    {
        return Create(typeof(T)) as T;
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
        return Activator.CreateInstance(type, true);
    }
}