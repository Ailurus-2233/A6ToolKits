using A6ToolKits.Database.Enums;

// ReSharper disable ClassNeverInstantiated.Global

namespace A6ToolKits.Database.Attributes;

/// <summary>
///     列类型特性
/// </summary>
/// <param name="name">
///     列名
/// </param>
/// <param name="columnType">
///     列类型
/// </param>
/// <param name="dataLength">
///     数据长度，只在特殊的类型中生效
/// </param>
[AttributeUsage(AttributeTargets.Property)]
public class ColumnTypeAttribute(string name, ColumnType columnType, int dataLength = 0) : Attribute
{
    /// <summary>
    ///     列名
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    ///     列类型
    /// </summary>
    public ColumnType ColumnType { get; } = columnType;

    /// <summary>
    ///     数据长度
    /// </summary>
    public int DataLength { get; } = dataLength;

    /// <summary>
    ///     是否是文件数据库的列
    /// </summary>
    /// <returns>
    ///     true 是文件数据库的列, false 不是文件数据库的列
    /// </returns>
    public bool IsFileColumn()
    {
        var typeString = ColumnType.ToString();
        return typeString.StartsWith("FILE_");
    }
    
    /// <summary>
    ///     是否是SQLite列
    /// </summary>
    /// <returns>
    ///     返回是否是SQLite列
    /// </returns>
    public bool IsSQLiteColumn()
    {
        var typeString = ColumnType.ToString();
        return typeString.StartsWith("SQLITE_");
    }

    /// <summary>
    ///     是否是MySQL列
    /// </summary>
    /// <returns>
    ///     返回是否是MySQL列
    /// </returns>
    public bool IsMySQLColumn()
    {
        var typeString = ColumnType.ToString();
        return typeString.StartsWith("MYSQL_");
    }

    private readonly Dictionary<ColumnType, List<Type>> _availableTypes = new()
    {
        { ColumnType.FILE_STRING, [typeof(string), typeof(char)] },
        {
            ColumnType.FILE_INTEGER,
            [
                typeof(byte), typeof(short), typeof(int), typeof(long), typeof(sbyte), typeof(ushort), typeof(uint),
                typeof(ulong)
            ]
        },
        { ColumnType.File_FLOAT, [typeof(float), typeof(double)] },
        { ColumnType.File_BOOLEAN, [typeof(bool)] },

        { ColumnType.SQLITE_NULL, [] },
        {
            ColumnType.SQLITE_INTEGER,
            [
                typeof(byte), typeof(short), typeof(int), typeof(long), typeof(sbyte), typeof(ushort), typeof(uint),
                typeof(ulong)
            ]
        },
        { ColumnType.SQLITE_REAL, [typeof(float), typeof(double)] },
        { ColumnType.SQLITE_TEXT, [typeof(string)] },
        { ColumnType.SQLITE_BLOB, [typeof(byte[]), typeof(sbyte[])] },

        { ColumnType.MYSQL_TINYINT, [typeof(sbyte)] },
        { ColumnType.MYSQL_SMALLINT, [typeof(short)] },
        { ColumnType.MYSQL_MEDIUMINT, [typeof(int)] },
        { ColumnType.MYSQL_INT, [typeof(int)] },
        { ColumnType.MYSQL_BIGINT, [typeof(long)] },
        { ColumnType.MYSQL_FLOAT, [typeof(float)] },
        { ColumnType.MYSQL_DOUBLE, [typeof(double)] },
        { ColumnType.MYSQL_DECIMAL, [typeof(decimal)] },
        { ColumnType.MYSQL_DATE, [typeof(DateTime)] },
        { ColumnType.MYSQL_TIME, [typeof(TimeSpan)] },
        { ColumnType.MYSQL_YEAR, [typeof(int)] },
        { ColumnType.MYSQL_DATETIME, [typeof(DateTime)] },
        { ColumnType.MYSQL_TIMESTAMP, [typeof(DateTime)] },
        { ColumnType.MYSQL_CHAR, [typeof(string)] },
        { ColumnType.MYSQL_VARCHAR, [typeof(string)] },
        { ColumnType.MYSQL_TINYTEXT, [typeof(string)] },
        { ColumnType.MYSQL_TEXT, [typeof(string)] },
        { ColumnType.MYSQL_MEDIUMTEXT, [typeof(string)] },
        { ColumnType.MYSQL_LONGTEXT, [typeof(string)] },
        { ColumnType.MYSQL_BINARY, [typeof(byte[]), typeof(sbyte[])] },
        { ColumnType.MYSQL_VARBINARY, [typeof(byte[]), typeof(sbyte[])] },
        { ColumnType.MYSQL_TINYBLOB, [typeof(byte[]), typeof(sbyte[])] },
        { ColumnType.MYSQL_BLOB, [typeof(byte[]), typeof(sbyte[])] },
        { ColumnType.MYSQL_MEDIUMBLOB, [typeof(byte[]), typeof(sbyte[])] },
        { ColumnType.MYSQL_LONGBLOB, [typeof(byte[]), typeof(sbyte[])] },
        { ColumnType.MYSQL_BOOLEAN, [typeof(bool)] },
        { ColumnType.MYSQL_JSON, [typeof(string)] },
    };

    /// <summary>
    ///     类型检查
    /// </summary>
    /// <param name="value">
    ///     属性值
    /// </param>
    /// <returns>
    ///     返回是否通过类型检查
    /// </returns>
    public bool TypeCheck(object? value)
    {
        if (value == null) return false;
        return _availableTypes.TryGetValue(ColumnType, out var type) && type.Contains(value.GetType());
    }
}