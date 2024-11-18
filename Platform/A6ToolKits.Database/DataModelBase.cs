using System.Reflection;
using System.Text;
using System.Xml;
using A6ToolKits.Database.Attributes;
using A6ToolKits.Database.Enums;
using A6ToolKits.Database.Exceptions;

namespace A6ToolKits.Database;

/// <summary>
///     数据模型基类
/// </summary>
public class DataModelBase : IData
{
    private List<PropertyInfo> GetPrimaryKeys()
    {
        var temp = GetAllColumns();
        return temp.Where(
            p => p.GetCustomAttribute<PrimaryKeyAttribute>() != null).ToList();
    }

    private List<PropertyInfo> GetColumns()
    {
        return GetAllColumns().Where(
            p => p.GetCustomAttribute<PrimaryKeyAttribute>() == null).ToList();
    }

    private List<PropertyInfo> GetAllColumns()
    {
        var temp = GetType().GetProperties().Where(
            p => p.GetCustomAttribute<ColumnTypeAttribute>() != null).ToList();
        var result = new List<PropertyInfo>();
        foreach (var property in temp)
        {
            var columnType = property.GetCustomAttribute<ColumnTypeAttribute>();
            if (columnType == null) continue;
            var value = property.GetValue(this);
            if (!columnType.TypeCheck(value))
                throw new TypeInconsistentException(columnType.ColumnType, property.PropertyType);
            result.Add(property);
        }

        return result;
    }

    /// <summary>
    ///     转化为一个 <see cref="XmlElement" /> 对象，记录当前对象中所有
    ///     public 属性且包含列类型属性 <see cref="ColumnTypeAttribute"/>
    ///     的值，除了带有 <see cref="ExcludeAttribute" /> 的属性，根据
    ///     列类型属性的值进行转换，不考虑复杂类型的存储
    /// </summary>
    /// <returns>
    ///     转化后的 <see cref="XmlDocument" /> 对象
    /// </returns>
    public XmlElement ToXml()
    {
        var xml = new XmlDocument();
        var root = xml.CreateElement(GetType().Name);
        var primaryKeys = GetPrimaryKeys();
        // 将主键添加到节点中
        var primaryKeyElement = xml.CreateElement("PrimaryKeys");
        foreach (var primaryKey in primaryKeys)
        {
            if (primaryKey.GetCustomAttribute<ColumnTypeAttribute>()!.IsXMLColumn()) continue;
            var primaryKeyElementItem = CreateXmlElement(xml, primaryKey);
            if (primaryKeyElementItem == null) continue;
            primaryKeyElement.AppendChild(primaryKeyElementItem);
        }

        root.AppendChild(primaryKeyElement);

        // 将非主键添加到节点中
        var columns = GetColumns();
        foreach (var column in columns)
        {
            if (column.GetCustomAttribute<ColumnTypeAttribute>()!.IsXMLColumn()) continue;
            var columnElement = CreateXmlElement(xml, column);
            if (columnElement == null) continue;
            root.AppendChild(columnElement);
        }

        return root;
    }

    private XmlElement? CreateXmlElement(XmlDocument xml, PropertyInfo property)
    {
        var columnType = property.GetCustomAttribute<ColumnTypeAttribute>();
        var element = xml.CreateElement(property.Name);
        var value = property.GetValue(this);
        element.SetAttribute("Type", columnType?.ColumnType.ToString());
        element.InnerText = value?.ToString() ?? string.Empty;
        return element;
    }

    /// <summary>
    ///     从XML中加载数据，只支持声明的 String, Integer, Float, Boolean 类型的加载
    /// </summary>
    /// <param name="xml">
    ///     XML数据
    /// </param>
    public void FromXml(XmlElement xml)
    {
        var properties = GetType().GetProperties();
        foreach (var property in properties)
        {
            var columnType = property.GetCustomAttribute<ColumnTypeAttribute>();
            if (columnType == null || !columnType.IsXMLColumn()) continue;

            var element = xml.SelectSingleNode(property.Name);
            if (element == null) continue;

            var value = element.InnerText;
            if (string.IsNullOrEmpty(value)) continue;
            if (property.CanWrite)
                property.SetValue(this, Convert.ChangeType(value, property.PropertyType));
        }
    }

    public string ToJson()
    {
        throw new NotImplementedException();
    }

    public void FromJson(string json)
    {
        throw new NotImplementedException();
    }

    public string GetCSVHeader()
    {
        throw new NotImplementedException();
    }

    public string ToCSVLine()
    {
        throw new NotImplementedException();
    }

    public void FromCSVLine(string csvLine)
    {
        throw new NotImplementedException();
    }
}