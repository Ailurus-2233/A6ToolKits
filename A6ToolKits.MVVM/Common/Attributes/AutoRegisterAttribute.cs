namespace A6ToolKits.MVVM.Common.Attributes;

/// <summary>
/// 一个类的标记，用于自动注册到IoC容器中
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class AutoRegisterAttribute(Type? interfaceType = null, RegisterType type = RegisterType.Transient)
    : Attribute
{
    public RegisterType RegisterType { get; } = type;

    public Type? InterfaceType { get; } = interfaceType ?? typeof(object);
}

public enum RegisterType
{
    Transient,
    Singleton
}