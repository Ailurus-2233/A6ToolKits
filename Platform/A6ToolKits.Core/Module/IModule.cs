namespace A6ToolKits.Module;

/// <summary>
///     模块接口
/// </summary>
public interface IModule
{
    /// <summary>
    ///     模块初始化
    /// </summary>
    protected void Initialize();

    /// <summary>
    ///     模块加载流程
    /// </summary>
    public void LoadModule();
}