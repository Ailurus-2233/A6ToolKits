using System;

namespace A6ToolKits.Helper.Instance;

/// <summary>
///     实例创建器接口
/// </summary>
public interface IInstanceHelper
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
    public object? CreateInstance(string type, string? assembly = null);

    /// <summary>
    ///     从特定位置获取指定类型的实例，如果不存在则创建一个新的实例
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
    public object? GetOrCreateInstance(string type, string? assembly = null);

    /// <summary>
    ///     创建指定类型的实例
    /// </summary>
    /// <typeparam name="T">
    ///     类型
    /// </typeparam>
    /// <returns>
    ///     返回类型的实例，如果创建失败则返回 null
    /// </returns>
    public T? CreateInstance<T>() where T : class;


    /// <summary>
    ///     从特定位置获取指定类型的实例，如果不存在则创建一个新的实例
    /// </summary>
    /// <typeparam name="T">
    ///     类型
    /// </typeparam>
    /// <returns>
    ///     返回类型的实例，如果创建失败则返回 null
    /// </returns>
    public T? GetOrCreateInstance<T>() where T : class;

    /// <summary>
    ///     创建指定类型的实例
    /// </summary>
    /// <param name="type">
    ///     类型
    /// </param>
    /// <returns>
    ///     返回类型的实例，如果创建失败则返回 null
    /// </returns>
    public object? CreateInstance(Type type);


    /// <summary>
    ///     从特定位置获取指定类型的实例，如果不存在则创建一个新的实例
    /// </summary>
    /// <param name="type">
    ///     指定类型
    /// </param>
    /// <returns>
    ///     返回类型的实例，如果创建失败则返回 null
    /// </returns>
    public object? GetOrCreateInstance(Type type);
}