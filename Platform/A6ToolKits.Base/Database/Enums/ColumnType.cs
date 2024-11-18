// ReSharper disable InconsistentNaming
namespace A6ToolKits.Database.Enums;

/// <summary>
///     对应数据库的目标数据类型
/// </summary>
public enum ColumnType
{
    // SQLite
    SQLITE_NULL,
    SQLITE_INTEGER,
    SQLITE_REAL,
    SQLITE_TEXT,
    SQLITE_BLOB,
    
    // MySQL
    MYSQL_TINYINT,
    MYSQL_SMALLINT,
    MYSQL_MEDIUMINT,
    MYSQL_INT,
    MYSQL_BIGINT,
    
    MYSQL_FLOAT,
    MYSQL_DOUBLE,
    MYSQL_DECIMAL,
    
    MYSQL_DATE,
    MYSQL_TIME,
    MYSQL_YEAR,
    MYSQL_DATETIME,
    MYSQL_TIMESTAMP,
    
    MYSQL_CHAR,
    MYSQL_VARCHAR,
    MYSQL_TINYTEXT,
    MYSQL_TEXT,
    MYSQL_MEDIUMTEXT,
    MYSQL_LONGTEXT,
    
    MYSQL_BINARY,
    MYSQL_VARBINARY,
    MYSQL_TINYBLOB,
    MYSQL_BLOB,
    MYSQL_MEDIUMBLOB,
    MYSQL_LONGBLOB,
    
    MYSQL_BOOLEAN,
    MYSQL_JSON,
    
    // XML
    XML_STRING,
    XML_INTEGER,
    XML_FLOAT,
    XML_BOOLEAN,
}