using System.Reflection;
using System.Xml;
using A6ToolKits.Database.Attributes;

namespace A6ToolKits.Database.DataConverters;

/// <summary>
///     XML转换器
/// </summary>
public static class XmlConverter
{
    /// <summary>
    ///     转化为一个 <see cref="XmlElement" /> 对象，记录当前对象中所有
    ///     public 属性且包含列类型属性 <see cref="ColumnTypeAttribute"/>
    ///     的值，除了带有 <see cref="ExcludeAttribute" /> 的属性，根据
    ///     列类型属性的值进行转换，不考虑复杂类型的存储
    /// </summary>
    /// <returns>
    ///     转化后的 <see cref="XmlDocument" /> 对象
    /// </returns>
    public static XmlElement ToXml(this FileDataModelBase data)
    {
        var xml = new XmlDocument();
        var root = xml.CreateElement(data.GetType().Name);
        var primaryKeys = data.GetPrimaryKeys();
        // 将主键添加到节点中
        var primaryKeyElement = xml.CreateElement("PrimaryKeys");
        foreach (var primaryKey in primaryKeys)
        {
            if (primaryKey.GetCustomAttribute<ColumnTypeAttribute>()!.IsXMLColumn()) continue;
            var primaryKeyElementItem = data.CreateXmlElement(xml, primaryKey);
            if (primaryKeyElementItem == null) continue;
            primaryKeyElement.AppendChild(primaryKeyElementItem);
        }

        root.AppendChild(primaryKeyElement);

        // 将非主键添加到节点中
        var columns = data.GetNonPrimaryKey();
        foreach (var column in columns)
        {
            if (column.GetCustomAttribute<ColumnTypeAttribute>()!.IsXMLColumn()) continue;
            var columnElement = data.CreateXmlElement(xml, column);
            if (columnElement == null) continue;
            root.AppendChild(columnElement);
        }

        return root;
    }

    private static XmlElement? CreateXmlElement(this FileDataModelBase data, XmlDocument xml, PropertyInfo property)
    {
        var columnType = property.GetCustomAttribute<ColumnTypeAttribute>();
        var element = xml.CreateElement(property.Name);
        var value = property.GetValue(data);
        element.SetAttribute("Type", columnType?.ColumnType.ToString());
        element.InnerText = value?.ToString() ?? string.Empty;
        return element;
    }

    /// <summary>
    ///     从XML中加载数据，只支持声明的 String, Integer, Float, Boolean 类型的加载
    /// </summary>
    /// <param name="data">
    ///     数据对象
    /// </param>
    /// <param name="xml">
    ///     XML数据
    /// </param>
    public static void FromXml(this FileDataModelBase data, XmlElement xml)
    {
        var properties = data.GetType().GetProperties();
        foreach (var property in properties)
        {
            var columnType = property.GetCustomAttribute<ColumnTypeAttribute>();
            if (columnType == null || !columnType.IsXMLColumn()) continue;

            var element = xml.SelectSingleNode(property.Name);
            if (element == null) continue;

            var value = element.InnerText;
            if (string.IsNullOrEmpty(value)) continue;
            if (property.CanWrite)
                property.SetValue(data, Convert.ChangeType(value, property.PropertyType));
        }
    }
}