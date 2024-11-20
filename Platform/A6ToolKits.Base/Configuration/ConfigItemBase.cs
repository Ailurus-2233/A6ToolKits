using System.Xml;

namespace A6ToolKits.Configuration;

/// <summary>
///     配置项基类，利用反射可以从 XmlNode 生成配置项
/// </summary>
public abstract class ConfigItemBase : IConfigItem
{
    /// <summary>
    ///     从配置文件加载配置项
    /// </summary>
    public abstract void LoadConfig();

    /// <summary>
    ///     创建一个默认的配置项
    /// </summary>
    /// <param name="tagName">
    ///     配置项的标签名
    /// </param>
    /// <returns>
    ///     返回一个默认的配置项
    /// </returns>
    public XmlElement CreateDefaultConfig(string tagName)
    {
        var doc = ConfigHelper.GetDefaultConfig();
        var result = doc.CreateElement(tagName);
        SetDefault();
        var property = GetType().GetProperties();
        foreach (var prop in property)
        {
            var value = prop.GetValue(this);
            if (value != null) result.SetAttribute(prop.Name, value.ToString());
        }

        return result;
    }

    /// <summary>
    ///     设置配置项的默认值
    /// </summary>
    public abstract void SetDefault();

    /// <summary>
    ///     从 XmlNode 生成配置项
    /// </summary>
    /// <param name="node">
    ///     XmlNode
    /// </param>
    protected void GenerateFromXmlNode(XmlNode node)
    {
        var property = GetType().GetProperties();
        foreach (var prop in property)
        {
            var value = node.Attributes?[prop.Name]?.Value;
            if (value != null) prop.SetValue(this, Convert.ChangeType(value, prop.PropertyType));
        }
    }
}