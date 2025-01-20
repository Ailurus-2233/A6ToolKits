using System.Xml;

namespace A6Toolkits.Configuration;

/// <summary>
///     配置项接口
/// </summary>
public interface IConfigItem
{
    /// <summary>
    ///     配置项的子配置项
    /// </summary>
    List<IConfigItem> Children { get; }

    /// <summary>
    ///     配置项是否加载完成
    /// </summary>
    bool LoadedFinished { get; set; }

    /// <summary>
    ///     配置项是否必要
    /// </summary>
    bool IsNecessary { get; }

    /// <summary>
    ///     从配置文件加载配置项
    /// </summary>
    void LoadConfig();

    /// <summary>
    ///     配置项加载完成后的操作
    /// </summary>
    void OnLoadedConfig();

    /// <summary>
    ///     创建一个默认的配置项
    /// </summary>
    /// <returns>
    ///     返回一个默认的配置项
    /// </returns>
    XmlElement CreateDefaultConfig(XmlDocument root);
    
    /// <summary>
    ///     生成 XmlNode
    /// </summary>
    /// <param name="root">
    ///     XmlDocument
    /// </param>
    /// <returns>
    ///     返回生成的 XmlNode
    /// </returns>
    XmlElement GenerateXmlNode(XmlDocument root);

    /// <summary>
    ///     设置配置项的默认值
    /// </summary>
    void SetDefault();
    
    /// <summary>
    ///     保存配置项到配置文件
    /// </summary>
    void SaveConfig();
}