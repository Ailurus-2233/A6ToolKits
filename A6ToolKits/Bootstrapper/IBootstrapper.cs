namespace A6ToolKits.Bootstrapper;

/// <summary>
///     启动类接口
/// </summary>
public interface IBootstrapper
{
    /// <summary>
    ///     初始化
    /// </summary>
    void Initialize();

    /// <summary>
    ///     配置
    /// </summary>
    void Configure();

    /// <summary>
    ///     完成
    /// </summary>
    void OnCompleted();

    /// <summary>
    ///     运行
    /// </summary>
    /// <param name="args">
    ///     命令行参数
    /// </param>
    void Run(string[] args);
}