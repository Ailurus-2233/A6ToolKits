using A6ToolKits.Database.Configs;

namespace A6ToolKits.Database.Managers;

/// <summary>
///     SQLite 数据库管理器基类
/// </summary>
public class SQLiteDatabaseManager : DatabaseManagerBase
{
    /// <summary>
    ///     构造函数
    /// </summary>
    /// <param name="config">
    ///     SQLite 配置项
    /// </param>
    public SQLiteDatabaseManager(SQLiteConfigItem config)
    {
        ConnectionString = config.ToConnectionString();
    }
    
    public override void Save<T>(IList<T> data, bool force = false)
    {
        throw new NotImplementedException();
    }

    public override void Add<T>(IList<T> data)
    {
        throw new NotImplementedException();
    }

    public override void Add<T>(params T[] data)
    {
        throw new NotImplementedException();
    }

    public override IList<T> Load<T>()
    {
        throw new NotImplementedException();
    }

    public override void Delete<T>(IList<T> target)
    {
        throw new NotImplementedException();
    }

    public override void Delete<T>(params T[] target)
    {
        throw new NotImplementedException();
    }

    public override void Update<T>(IList<T> data)
    {
        throw new NotImplementedException();
    }

    public override void Update<T>(params T[] data)
    {
        throw new NotImplementedException();
    }
}