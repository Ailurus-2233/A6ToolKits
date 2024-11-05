namespace A6ToolKits.Bootstrapper.Interfaces;

/// <summary>
///     启动器接口
/// </summary>
public interface IBootstrapper
{
    /// <summary>
    ///     加载步骤1-初始化：在应用启动前需要进行的一些配置
    /// </summary>
    public void Initialize();

    /// <summary>
    ///     加载步骤2-配置：在初始化完成后，需要进行的一些配置
    /// </summary>
    public void Configure();

    /// <summary>
    ///     加载步骤3-完成：在配置完成后，需要进行的一些操作
    /// </summary>
    public void OnCompleted();

    /// <summary>
    ///     结束步骤：在程序退出时，需要执行的一些操作
    /// </summary>
    public void OnFinished();
}