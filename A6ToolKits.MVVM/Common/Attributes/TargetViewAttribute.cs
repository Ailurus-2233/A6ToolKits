namespace A6ToolKits.MVVM.Common.Attributes;

/// <summary>
/// 一个类的标记，用于指定ViewModel对应的View，并将 View 和 ViewModel 自动注册到IoC容器中, 默认为单例
/// 如果是瞬时注册，需要在使用时手动给 View.DataContext 赋值
/// </summary>
/// <param name="viewType"></param>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class TargetViewAttribute(Type viewType, RegisterType type = RegisterType.Singleton) : Attribute
{
    public Type ViewType { get; set; } = viewType;

    public RegisterType RegisterType { get; set; } = type;
}