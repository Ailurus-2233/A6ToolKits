namespace A6ToolKits.Config;

/// <summary>
///     配置项接口，用于配置文件的读写
/// </summary>
public interface IConfigItem
{
    /// <summary>
    ///     生成默认配置
    /// </summary>
    /// <returns>
    ///     返回一个默认的配置项
    /// </returns>
    IConfigItem GenerateDefaultConfig();
    
    /// <summary>
    ///     从配置文件加载配置项
    /// </summary>
    void LoadConfig();
    
    /// <summary>
    ///     保存配置项到配置文件
    /// </summary>
    void SaveConfig();
}