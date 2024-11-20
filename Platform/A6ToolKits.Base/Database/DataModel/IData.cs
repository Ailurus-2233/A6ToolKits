using System.Reflection;

namespace A6ToolKits.Database.DataModel;

/// <summary>
///     数据存储需要实现的读写接口接口
/// </summary>
public interface IData
{
    /// <summary>
    ///     获取主键属性列表，属性中有PrimaryKeyAttribute特性
    /// </summary>
    /// <returns>
    ///     主键属性列表
    /// </returns>
    List<PropertyInfo> GetPrimaryKeys();

    /// <summary>
    ///     获取非主键属性列表，属性中没有PrimaryKeyAttribute特性
    /// </summary>
    /// <returns>
    ///     非主键属性列表
    /// </returns>
    List<PropertyInfo> GetNonPrimaryKey();

    /// <summary>
    ///     获取所有被标注的列，属性中有ColumnTypeAttribute特性
    /// </summary>
    /// <returns>
    ///     所有列属性列表
    /// </returns>
    List<PropertyInfo> GetAllColumns();
}