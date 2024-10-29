using System;

namespace A6ToolKits.Instance;

/// <summary>
///     实例创建器接口
/// </summary>
public interface IInstanceCreator
{
    /// <summary>
    ///     创建指定类型的实例
    /// </summary>
    /// <param name="assembly">
    ///     类型所在的程序集名称，如果为 null 则从当前程序集中查找
    /// </param>
    /// <param name="type">
    ///     类型名称
    /// </param>
    /// <returns>
    ///     返回类型的实例，如果创建失败则返回 null
    /// </returns>
    public object? Create(string type, string? assembly = null);

    /// <summary>
    ///     创建指定类型的实例
    /// </summary>
    /// <typeparam name="T">
    ///     类型
    /// </typeparam>
    /// <returns>
    ///     返回类型的实例，如果创建失败则返回 null
    /// </returns>
    public T? Create<T>() where T : class;
    
    /// <summary>
    ///     创建指定类型的实例
    /// </summary>
    /// <param name="type">
    ///     类型
    /// </param>
    /// <returns>
    ///     返回类型的实例，如果创建失败则返回 null
    /// </returns>
    public object? Create(Type type);
}