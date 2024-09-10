namespace A6ToolKits.MVVM.Common.Attributes;

/// <summary>
///     一个类的标记，用于指定ViewModel对应的View，并将 View 和 ViewModel 自动注册到IoC容器中, 默认为单例
///     如果是瞬时注册，需要在使用时手动给 View.DataContext 赋值
/// </summary>
/// <param name="viewType"> View 的类型 </param>
/// <param name="type"> 注册的方式，默认为Singleton </param>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class TargetViewAttribute(Type viewType, RegisterType type = RegisterType.Singleton) : Attribute
{
    /// <summary>
    ///     View 的类型
    /// </summary>
    public Type ViewType { get; set; } = viewType;

    /// <summary>
    ///     注册的方式，默认为Singleton
    /// </summary>
    public RegisterType RegisterType { get; set; } = type;
}