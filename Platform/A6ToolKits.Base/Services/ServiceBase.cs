namespace A6ToolKits.Services;

/// <summary>
///     服务类型的基类，用于标记一个类为服务
/// </summary>
public abstract class ServiceBase : IService
{
    /// <inheritdoc />
    public abstract void Initialize();

    /// <inheritdoc />
    public abstract void OnExit();
}