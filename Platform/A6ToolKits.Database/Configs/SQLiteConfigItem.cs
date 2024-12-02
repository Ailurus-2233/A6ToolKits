using A6ToolKits.Configuration;
using A6ToolKits.Configuration.Exceptions;

namespace A6ToolKits.Database.Configs;

/// <summary>
///     SQLite 配置
/// </summary>
public class SQLiteConfigItem : ConfigItemBase
{
    /// <summary>
    ///     Data Source: （必需）指定数据库文件的路径。如果数据库文件不存在，SQLite 会在执行时创建一个新的数据库文件。
    /// </summary>
    public string DataSource { get; set; } = "";

    /// <summary>
    ///     Version: 指定 SQLite 数据库的版本。通常不需要设置，默认为 3。
    /// </summary>
    public string Version { get; set; } = "3";

    /// <summary>
    ///     Pooling: 指定是否启用连接池。默认为 True。
    /// </summary>
    public string Pooling { get; set; } = "True";

    /// <summary>
    ///     Max Pool Size: 指定连接池中最大连接数。默认值通常是 100。
    /// </summary>
    public string MaxPoolSize { get; set; } = "100";

    /// <summary>
    ///     Read Only: 指定数据库是否以只读模式打开。
    /// </summary>
    public string ReadOnly { get; set; } = "False";

    /// <summary>
    ///     Password: 为加密的数据库指定密码。
    /// </summary>
    public string Password { get; set; } = "";

    /// <summary>
    ///     Cache Size: 控制 SQLite 的缓存大小。
    /// </summary>
    public string CacheSize { get; set; } = "20480";

    /// <summary>
    ///     Synchronous: 控制数据库的同步模式。可以设置为 Normal、Full 或 Off。
    /// </summary>
    public string SyncMode { get; set; } = "Normal";

    /// <summary>
    ///     Timeout: 指定连接超时时间（以毫秒为单位）。
    /// </summary>
    public string Timeout { get; set; } = "5000";

    /// <summary>
    ///     Journal Mode: 控制SQLite使用的日志模式。可以设置为 Delete、Truncate、Persist、Memory、WAL（写前日志）。
    /// </summary>
    public string JournalMode { get; set; } = "Delete";

    /// <summary>
    ///     转换为连接字符串
    /// </summary>
    /// <returns>
    ///     连接字符串
    /// </returns>
    public string ToConnectionString()
    {
        return $"Data Source={DataSource};Version={Version};Pooling={Pooling};Max Pool Size={MaxPoolSize};" +
               $"Read Only={ReadOnly};Password={Password};Cache Size={CacheSize};Synchronous={SyncMode};" +
               $"Timeout={Timeout};Journal Mode={JournalMode};";
    }

    /// <inheritdoc />
    public override void LoadConfig()
    {
        var itemNode = ConfigHelper.GetElements("SQLite")?.Item(0);
        if (itemNode == null)
            throw new ConfigLoadException(typeof(SQLiteConfigItem));
        GenerateFromXmlNode(itemNode);
    }

    /// <inheritdoc />
    public override void SetDefault()
    {
        DataSource = "";
        Version = "3";
        Pooling = "True";
        MaxPoolSize = "100";
        ReadOnly = "False";
        Password = "";
        CacheSize = "20480";
        SyncMode = "Normal";
        Timeout = "5000";
        JournalMode = "Delete";
    }
}