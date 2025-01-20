namespace A6ToolKits.Attributes;

/// <summary>
///     一个类的标记，用于自动注册到IoC容器中
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class AutoRegisterAttribute(Type? interfaceType = null, RegisterType type = RegisterType.Transient)
    : AttributeBase
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
    /// <summary>
    ///     瞬时注册模式
    /// </summary>
    Transient,

    /// <summary>
    ///     单例注册模式
    /// </summary>
    Singleton
}