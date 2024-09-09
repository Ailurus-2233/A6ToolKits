﻿namespace A6ToolKits.MVVM.Common.Attributes;

/// <summary>
///     一个类的标记，用于自动注册到IoC容器中
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class AutoRegisterAttribute(Type? interfaceType = null, RegisterType type = RegisterType.Transient)
    : Attribute
{
    /// <summary>
    ///     注册的方式，默认为Transient
    /// </summary>
    public RegisterType RegisterType { get; } = type;

    /// <summary>
    ///     接口类型，默认为自身
    /// </summary>
    public Type? InterfaceType { get; } = interfaceType ?? typeof(object);
}

/// <summary>
///     注册的方式
/// </summary>
public enum RegisterType
{
    Transient, // 瞬时
    Singleton // 单例
}