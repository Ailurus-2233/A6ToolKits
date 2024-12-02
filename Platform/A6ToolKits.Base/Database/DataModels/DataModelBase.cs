using System.Reflection;
using System.Text;
using A6ToolKits.Database.Attributes;
using A6ToolKits.Database.Exceptions;

namespace A6ToolKits.Database.DataModels;

/// <summary>
///     数据库数据模型
/// </summary>
public class DataModelBase : IData
{
    /// <summary>
    ///     从当前属性中获取主键，是主键的条件是属性上有PrimaryKeyAttribute和ColumnTypeAttribute两个特性
    /// </summary>
    /// <returns>
    ///     主键列表
    /// </returns>
    public List<PropertyInfo> GetPrimaryKeys()
    {
        var temp = GetAllColumns();
        return temp.Where(
            p => p.GetCustomAttribute<PrimaryKeyAttribute>() != null).ToList();
    }

    /// <summary>
    ///     从当前属性中获取非主键，是非主键的条件是属性上只有ColumnTypeAttribute特性
    /// </summary>
    /// <returns>
    ///     非主键列表
    /// </returns>
    public List<PropertyInfo> GetNonPrimaryKey()
    {
        return GetAllColumns().Where(
            p => p.GetCustomAttribute<PrimaryKeyAttribute>() == null).ToList();
    }

    /// <summary>
    ///     获取所有列，列的条件是属性上有ColumnTypeAttribute特性
    /// </summary>
    /// <returns>
    ///     所有列
    /// </returns>
    /// <exception cref="TypeInconsistentException">
    ///     当属性的类型和ColumnTypeAttribute的类型不一致时抛出异常
    /// </exception>
    public List<PropertyInfo> GetAllColumns()
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
        
        return Equals((DataModelBase) other);
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
    protected bool Equals(DataModelBase other)
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