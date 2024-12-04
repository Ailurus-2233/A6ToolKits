using A6ToolKits.Database.Attributes;
using A6ToolKits.Database.Configs;
using A6ToolKits.Database.DataConverters;
using Microsoft.Data.Sqlite;

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
        var command = data[0].AddCommand();
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        using var transaction = connection.BeginTransaction();
        using var cmd = connection.CreateCommand();
        cmd.CommandText = command;

        var parameters = new Dictionary<string, SqliteParameter>();
        
        foreach (var property in data[0].GetAllColumns())
        {
            var parameter = cmd.CreateParameter();
            parameter.ParameterName = $"${property.GetColumnType().Name}";
            cmd.Parameters.Add(parameter);
            parameters.Add(property.GetColumnType().Name, parameter);
        }
        foreach (var item in data)
        {
            foreach (var property in item.GetAllColumns())
            {
                parameters[property.GetColumnType().Name].Value = property.GetValue(item);
            }
            cmd.ExecuteNonQuery();
        }
        transaction.Commit();
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

    public override List<T> Select<T>(Func<T, bool> query)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public override void CreateTable<T>(T data)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = data.CreateTableCommand();
        command.ExecuteNonQuery();
    }
}