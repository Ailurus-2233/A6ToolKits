using System.Xml;

namespace A6ToolKits.Configuration;

/// <summary>
///     配置项接口
/// </summary>
public interface IConfigItem
{
    /// <summary>
    ///     从配置文件加载配置项
    /// </summary>
    void LoadConfig();

    /// <summary>
    ///     创建一个默认的配置项
    /// </summary>
    /// <param name="tagName">
    ///     配置项的标签名
    /// </param>
    /// <returns>
    ///     返回一个默认的配置项
    /// </returns>
    XmlElement CreateDefaultConfig(string tagName);

    /// <summary>
    ///     设置配置项的默认值
    /// </summary>
    void SetDefault();
}