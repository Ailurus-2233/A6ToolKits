using System.Text;
using A6ToolKits.Database.Attributes;
using A6ToolKits.Database.DataModels;

namespace A6ToolKits.Database.DataConverters;

/// <summary>
///     数据类型对 SQL 语句的转化类
/// </summary>
public static class SQLConverter
{
    /// <summary>
    ///     生成创建表的 SQL 语句
    /// </summary>
    /// <param name="data">
    ///     数据类型
    /// </param>
    /// <returns>
    ///     创建表的 SQL 语句
    /// </returns>
    public static string CreateTableCommand(this IData data)
    {
        var primaryKeys = data.GetPrimaryKeys();
        var columns = data.GetNonPrimaryKey();

        var result = new StringBuilder($"CREATE TABLE IF NOT EXISTS {data.GetTableName()} (");

        foreach (var primary in primaryKeys)
        {
            result.Append($"{primary.Name} {primary.GetColumnType().GetSQLType()},");
        }

        foreach (var column in columns)
        {
            result.Append($"{column.Name} {column.GetColumnType().GetSQLType()},");
        }

        result.Append("PRIMARY KEY (");
        foreach (var primary in primaryKeys)
        {
            result.Append($"{primary.Name},");
        }

        result.Remove(result.Length - 1, 1);
        result.Append("));");

        return result.ToString();
    }
    
    /// <summary>
    ///     生成插入数据的 SQL 语句
    /// </summary>
    /// <param name="data">
    ///     数据对象
    /// </param>
    /// <returns>
    ///     插入数据的 SQL 语句
    /// </returns>
    public static string AddCommand(this IData data)
    {
        var primaryKeys = data.GetPrimaryKeys();
        var columns = data.GetNonPrimaryKey();

        var result = new StringBuilder($"INSERT INTO {data.GetTableName()} (");

        foreach (var primary in primaryKeys)
        {
            result.Append($"{primary.GetColumnType().Name},");
        }

        foreach (var column in columns)
        {
            result.Append($"{column.GetColumnType().Name},");
        }

        result.Remove(result.Length - 1, 1);
        result.Append(") VALUES (");

        foreach (var primary in primaryKeys)
        {
            result.Append($"${primary.GetColumnType().Name},");
        }

        foreach (var column in columns)
        {
            result.Append($"${column.GetColumnType().Name},");
        }

        result.Remove(result.Length - 1, 1);
        result.Append(");");

        return result.ToString();
    }
}