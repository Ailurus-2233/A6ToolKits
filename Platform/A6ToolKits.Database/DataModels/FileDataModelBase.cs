using System.Reflection;
using System.Text;
using System.Xml;
using A6ToolKits.Database.Attributes;
using A6ToolKits.Database.DataModel;
using A6ToolKits.Database.Enums;
using A6ToolKits.Database.Exceptions;

namespace A6ToolKits.Database;

/// <summary>
///     存储到文件的数据模型基类
/// </summary>
public abstract class FileDataModelBase : DataModelBase
{
    /// <summary>
    ///     判断两个数据模型是否相等
    /// </summary>
    /// <param name="other">
    ///     另一个数据模型
    /// </param>
    /// <returns>
    ///     true-两个数据模型相等
    ///     false-两个数据模型不相等
    /// </returns>
    public override bool Equals(object? other)
    {
        if (other == null)
            return false;
        if (other.GetType() != GetType())
            return false;
        
        return Equals((FileDataModelBase) other);
    }

    /// <summary>
    ///     比较两个数据模型是否相等
    /// </summary>
    /// <param name="other">
    ///     另一个数据模型
    /// </param>
    /// <returns>
    ///     true-两个数据模型相等
    ///     false-两个数据模型不相等
    /// </returns>
    protected bool Equals(FileDataModelBase other)
    {
        var hashCode = GetHashCode();
        return hashCode.Equals(other.GetHashCode());
    }

    /// <summary>
    ///     获取数据模型的哈希码
    /// </summary>
    /// <returns>
    ///     数据模型的哈希码
    /// </returns>
    public override int GetHashCode()
    {
        var primaryKeys = GetPrimaryKeys();
        var hashCode = new StringBuilder();
        foreach (var primaryKey in primaryKeys)
        {
            var value = primaryKey.GetValue(this);
            if (value != null)
                hashCode.Append(value);
        }

        return hashCode.ToString().GetHashCode();
    }
}