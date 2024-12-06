using A6ToolKits.Configuration;
using A6ToolKits.Configuration.Attributes;
using A6ToolKits.Configuration.Exceptions;
using A6ToolKits.Database.Managers;
using Microsoft.Data.Sqlite;

namespace A6ToolKits.Database.Configs;

/// <summary>
///     SQLite 配置
/// </summary>
[ConfigName("SQLite")]
public class SQLiteConfigItem : DatabaseConfigItemBase
{
    /// <inheritdoc />
    public override string Name { get; set; } = "database_sqlite";

    /// <inheritdoc />
    public override IManager GenerateManager()
    {
        return new SQLiteDatabaseManager(this);
    }

    /// <summary>
    ///     Data Source: （必需）指定数据库文件的路径。如果数据库文件不存在，SQLite 会在执行时创建一个新的数据库文件。
    ///     默认值为 ":memory:"，表示创建一个内存数据库。
    /// </summary>
    public string DataSource { get; set; } = ":memory:";

    /// <summary>
    ///     连接模式: 用于指定数据库连接的模式。默认值为 ReadWriteCreate。
    ///     可选值为:
    ///         ReadWriteCreate	打开数据库以进行读取和写入，如果数据库不存在，则创建数据库
    ///         ReadWrite	    打开数据库以进行读取和写入
    ///         ReadOnly		以只读模式打开数据库
    ///         Memory		    打开内存数据库
    /// </summary>
    public string Mode { get; set; } = "ReadWriteCreate";

    /// <summary>
    ///     连接使用的缓存模式: 用于指定数据库连接的缓存模式。默认值为 Default。
    ///     可选值为:
    ///         Default		使用基础 SQLite 库的默认模式
    ///         Private		每个连接使用一个专用缓存
    ///         Shared		连接共享一个缓存，此模式可更改事务和表锁定的行为
    /// </summary>
    public string Cache { get; set; } = "Default";

    /// <summary>
    ///     数据库密码: 用于指定数据库的密码。默认值为空字符串。
    /// </summary>
    public string Password { get; set; } = "";

    /// <summary>
    ///     是否启用外键约束: 用于指定是否启用外键约束。默认值为 True。
    /// </summary>
    public string ForeignKeys { get; set; } = "True";

    /// <summary>
    ///     是否启用递归触发器: 用于指定是否启用递归触发器。默认值为 False。
    /// </summary>
    public string RecursiveTriggers { get; set; } = "False";

    /// <summary>
    ///     默认超时时间: 用于指定数据库连接的默认超时时间（以秒为单位）。默认值为 30。
    /// </summary>
    public string DefaultTimeout { get; set; } = "30";

    /// <summary>
    ///     是否共用连接：用于指定是否共用连接。默认值为 True。
    /// </summary>
    public string Pooling { get; set; } = "True";

    /// <summary>
    ///     转换为连接字符串
    /// </summary>
    /// <returns>
    ///     连接字符串
    /// </returns>
    public string ToConnectionString()
    {
        return new SqliteConnectionStringBuilder
        {
            DataSource = DataSource,
            Mode = (SqliteOpenMode)Enum.Parse(typeof(SqliteOpenMode), Mode),
            Cache = (SqliteCacheMode)Enum.Parse(typeof(SqliteCacheMode), Cache),
            ForeignKeys = bool.Parse(ForeignKeys),
            RecursiveTriggers = bool.Parse(RecursiveTriggers),
            DefaultTimeout = int.Parse(DefaultTimeout),
            Pooling = bool.Parse(Pooling),
            Password = Password
        }.ToString();
    }


    /// <inheritdoc />
    public override bool IsNecessary { get; } = false;

    /// <inheritdoc />
    public override void SetDefault()
    {
        Name = "database_sqlite";
        DataSource = ":memory:";
        Mode = "ReadWriteCreate";
        Cache = "Default";
        Password = "";
        ForeignKeys = "True";
        RecursiveTriggers = "False";
        DefaultTimeout = "30";
        Pooling = "True";
    }

    /// <inheritdoc />
    public override void OnLoadedConfig()
    {
    }
}