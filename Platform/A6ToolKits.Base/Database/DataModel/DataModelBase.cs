using System.Reflection;
using A6ToolKits.Database.Attributes;
using A6ToolKits.Database.Exceptions;

namespace A6ToolKits.Database.DataModel;

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
}