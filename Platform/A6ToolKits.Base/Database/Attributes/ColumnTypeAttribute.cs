using A6ToolKits.Database.Enums;
// ReSharper disable ClassNeverInstantiated.Global

namespace A6ToolKits.Database.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ColumnTypeAttribute(string name, ColumnType columnType, int dataLength = 0) : Attribute
{
    public string Name { get; } = name;
    public ColumnType ColumnType { get; } = columnType;
    public int DataLength { get; } = dataLength;

    public bool IsXMLColumn()
    {
        return ColumnType is
            ColumnType.XML_STRING or
            ColumnType.XML_INTEGER or
            ColumnType.XML_FLOAT or
            ColumnType.XML_BOOLEAN;
    }

    public bool IsSQLiteColumn()
    {
        return ColumnType is
            ColumnType.SQLITE_NULL or
            ColumnType.SQLITE_INTEGER or
            ColumnType.SQLITE_REAL or
            ColumnType.SQLITE_TEXT or
            ColumnType.SQLITE_BLOB;
    }

    public bool IsMySQLColumn()
    {
        return ColumnType is
            ColumnType.MYSQL_TINYINT
            or ColumnType.MYSQL_SMALLINT
            or ColumnType.MYSQL_MEDIUMINT
            or ColumnType.MYSQL_INT
            or ColumnType.MYSQL_BIGINT
            or ColumnType.MYSQL_FLOAT
            or ColumnType.MYSQL_DOUBLE
            or ColumnType.MYSQL_DECIMAL
            or ColumnType.MYSQL_DATE
            or ColumnType.MYSQL_TIME
            or ColumnType.MYSQL_YEAR
            or ColumnType.MYSQL_DATETIME
            or ColumnType.MYSQL_TIMESTAMP
            or ColumnType.MYSQL_CHAR
            or ColumnType.MYSQL_VARCHAR
            or ColumnType.MYSQL_TINYTEXT
            or ColumnType.MYSQL_TEXT
            or ColumnType.MYSQL_MEDIUMTEXT
            or ColumnType.MYSQL_LONGTEXT
            or ColumnType.MYSQL_BINARY
            or ColumnType.MYSQL_VARBINARY
            or ColumnType.MYSQL_TINYBLOB
            or ColumnType.MYSQL_BLOB
            or ColumnType.MYSQL_MEDIUMBLOB
            or ColumnType.MYSQL_LONGBLOB
            or ColumnType.MYSQL_BOOLEAN
            or ColumnType.MYSQL_JSON;
    }
    
    public bool TypeCheck(object? value)
    {
        return ColumnType switch
        {
            ColumnType.XML_STRING => value is string,
            ColumnType.XML_INTEGER => value is byte or short or int or long or sbyte or ushort or uint or ulong,
            ColumnType.XML_FLOAT => value is float or double,
            ColumnType.XML_BOOLEAN => value is bool,
            ColumnType.SQLITE_NULL => false,
            ColumnType.SQLITE_INTEGER => value is byte or short or int or long or sbyte or ushort or uint or ulong,
            ColumnType.SQLITE_REAL => value is float or double,
            ColumnType.SQLITE_TEXT => value is string,
            ColumnType.SQLITE_BLOB => value is byte[] or sbyte[],
            ColumnType.MYSQL_TINYINT => value is sbyte,
            ColumnType.MYSQL_SMALLINT => value is short,
            ColumnType.MYSQL_MEDIUMINT => value is int,
            ColumnType.MYSQL_INT => value is int,
            ColumnType.MYSQL_BIGINT => value is long,
            ColumnType.MYSQL_FLOAT => value is float,
            ColumnType.MYSQL_DOUBLE => value is double,
            ColumnType.MYSQL_DECIMAL => value is decimal,
            ColumnType.MYSQL_DATE => value is DateTime,
            ColumnType.MYSQL_TIME => value is TimeSpan,
            ColumnType.MYSQL_YEAR => value is int,
            ColumnType.MYSQL_DATETIME => value is DateTime,
            ColumnType.MYSQL_TIMESTAMP => value is DateTime,
            ColumnType.MYSQL_CHAR => value is string,
            ColumnType.MYSQL_VARCHAR => value is string,
            ColumnType.MYSQL_TINYTEXT => value is string,
            ColumnType.MYSQL_TEXT => value is string,
            ColumnType.MYSQL_MEDIUMTEXT => value is string,
            ColumnType.MYSQL_LONGTEXT => value is string,
            ColumnType.MYSQL_BINARY => value is byte[] or sbyte[],
            ColumnType.MYSQL_VARBINARY => value is byte[] or sbyte[],
            ColumnType.MYSQL_TINYBLOB => value is byte[] or sbyte[],
            ColumnType.MYSQL_BLOB => value is byte[] or sbyte[],
            ColumnType.MYSQL_MEDIUMBLOB => value is byte[] or sbyte[],
            ColumnType.MYSQL_LONGBLOB => value is byte[] or sbyte[],
            ColumnType.MYSQL_BOOLEAN => value is bool,
            ColumnType.MYSQL_JSON => value is string,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}