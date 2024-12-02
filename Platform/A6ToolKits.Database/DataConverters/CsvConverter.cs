using System.Reflection;
using A6ToolKits.Database.Attributes;
using A6ToolKits.Database.DataModels;

namespace A6ToolKits.Database.DataConverters;

/// <summary>
///     CSV 数据转换器
/// </summary>
public static class CsvConverter
{
    /// <summary>
    ///     获取 CSV 文件的头部
    /// </summary>
    /// <param name="data">
    ///     数据
    /// </param>
    /// <param name="split">
    ///     分隔符
    /// </param>
    /// <returns>
    ///     CSV 文件的头部
    /// </returns>
    public static string GetCsvHeader(this IData data, char split)
    {
        var primaryKeys = data.GetPrimaryKeys();
        var result = string.Empty;
        foreach (var primary in primaryKeys)
        {
            if (!primary.GetCustomAttribute<ColumnTypeAttribute>()!.IsFileColumn()) continue;
            result += $"{primary.Name}{split}";
        }
        var columns = data.GetNonPrimaryKey();
        foreach (var column in columns)
        {
            if (!column.GetCustomAttribute<ColumnTypeAttribute>()!.IsFileColumn()) continue;
            result += $"{column.Name}{split}";
        }
        var temp = result.TrimEnd(split);
        return temp + Environment.NewLine;
    }

    /// <summary>
    ///     将数据转换为 CSV 行
    /// </summary>
    /// <param name="data">
    ///     数据
    /// </param>
    /// <param name="split">
    ///     分隔符
    /// </param>
    /// <returns>
    ///     CSV 行
    /// </returns>
    public static string ToCsvLine(this IData data, char split)
    {
        var primaryKeys = data.GetPrimaryKeys();
        var result = string.Empty;
        foreach (var primary in primaryKeys)
        {
            if (!primary.GetCustomAttribute<ColumnTypeAttribute>()!.IsFileColumn()) continue;
            result += $"{primary.GetValue(data)}{split}";
        }
        var columns = data.GetNonPrimaryKey();
        foreach (var column in columns)
        {
            if (!column.GetCustomAttribute<ColumnTypeAttribute>()!.IsFileColumn()) continue;
            result += $"{column.GetValue(data)}{split}";
        }
        var temp = result.TrimEnd(split);
        return temp + Environment.NewLine;
    }

    /// <summary>
    ///     从 CSV 行中加载数据
    /// </summary>
    /// <param name="data">
    ///     数据
    /// </param>
    /// <param name="csvLine">
    ///     CSV 行
    /// </param>
    /// <param name="split">
    ///     分隔符
    /// </param>
    public static void FromCsvLine(this IData data, string csvLine, char split)
    {
        var primaryKeys = data.GetPrimaryKeys();
        var columns = data.GetNonPrimaryKey();
        var values = csvLine.Split(split);
        var index = 0;
        foreach (var primary in primaryKeys)
        {
            if (!primary.GetCustomAttribute<ColumnTypeAttribute>()!.IsFileColumn()) continue;
            primary.SetValue(data, Convert.ChangeType(values[index++], primary.PropertyType));
        }
        foreach (var column in columns)
        {
            if (!column.GetCustomAttribute<ColumnTypeAttribute>()!.IsFileColumn()) continue;
            column.SetValue(data, Convert.ChangeType(values[index++], column.PropertyType));
        }
    }
}