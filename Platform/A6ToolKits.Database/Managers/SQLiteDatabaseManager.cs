using System.Linq.Expressions;
using A6ToolKits.Database.Attributes;
using A6ToolKits.Database.Configs;
using A6ToolKits.Database.DataConverters;
using A6ToolKits.Database.DataModels;
using A6ToolKits.Database.Exceptions;
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
    public SQLiteDatabaseManager(SQLiteConfigItem config) : base(config.Name)
    {
        ConnectionString = config.ToConnectionString();
    }

    /// <inheritdoc />
    public override void Add<T>(IList<T> data)
    {
        var command = data[0].GenerateInsertCommand();
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
                parameters[property.GetColumnType().Name].Value = property.GetValue(item);
            cmd.ExecuteNonQuery();
        }

        transaction.Commit();
    }

    /// <inheritdoc />
    public override IList<T> Load<T>()
    {
        var command = SQLiteConverter.GenerateSelectCommand<T>(p => true);
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        using var cmd = connection.CreateCommand();
        cmd.CommandText = command;
        using var reader = cmd.ExecuteReader();
        var result = new List<T>();
        while (reader.Read())
            result.Add(ReadItem<T>(reader));

        return result;
    }

    /// <inheritdoc />
    public override void Delete<T>(IList<T> target)
    {
        var command = target[0].GenerateDeleteCommand();
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        using var transaction = connection.BeginTransaction();
        using var cmd = connection.CreateCommand();
        cmd.CommandText = command;

        var parameters = new Dictionary<string, SqliteParameter>();

        foreach (var property in target[0].GetAllColumns())
        {
            var parameter = cmd.CreateParameter();
            parameter.ParameterName = $"${property.GetColumnType().Name}";
            cmd.Parameters.Add(parameter);
            parameters.Add(property.GetColumnType().Name, parameter);
        }

        foreach (var item in target)
        {
            foreach (var property in item.GetAllColumns())
                parameters[property.GetColumnType().Name].Value = property.GetValue(item);
            cmd.ExecuteNonQuery();
        }

        transaction.Commit();
    }

    /// <inheritdoc />
    public override void Clear<T>()
    {
        var command = SQLiteConverter.GenerateClearTableCommand<T>();
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        using var cmd = connection.CreateCommand();
        cmd.CommandText = command;
        cmd.ExecuteNonQuery();
    }


    /// <inheritdoc />
    public override void Update<T>(IList<T> data)
    {
        var command = data[0].GenerateUpdateCommand();
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
                parameters[property.GetColumnType().Name].Value = property.GetValue(item);
            cmd.ExecuteNonQuery();
        }

        transaction.Commit();
    }

    /// <inheritdoc />
    public override List<T> Select<T>(Expression<Func<T, bool>> predicate)
    {
        var command = SQLiteConverter.GenerateSelectCommand(predicate);
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        using var cmd = connection.CreateCommand();
        cmd.CommandText = command;
        using var reader = cmd.ExecuteReader();
        var result = new List<T>();
        while (reader.Read())
            result.Add(ReadItem<T>(reader));

        return result;
    }

    /// <inheritdoc />
    public override void CreateTable<T>(T data)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = data.GenerateCreateTableCommand();
        command.ExecuteNonQuery();
    }

    /// <summary>
    ///     读取数据项
    /// </summary>
    /// <param name="reader">
    ///     数据读取器
    /// </param>
    /// <typeparam name="T">
    ///     数据模型类型
    /// </typeparam>
    /// <returns>
    ///     数据项
    /// </returns>
    /// <exception cref="InvalidDataModelException">
    ///     数据模型无效
    /// </exception>
    public T ReadItem<T>(SqliteDataReader reader) where T : class, IData
    {
        var item = Activator.CreateInstance(typeof(T));
        if (item is not T dataModel)
            throw new InvalidDataModelException(typeof(T));
        foreach (var property in dataModel.GetAllColumns())
        {
            var columnType = property.GetColumnType();
            var value = reader[columnType.Name];
            if (value == DBNull.Value)
                continue;

            var type = property.PropertyType;
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                type = type.GetGenericArguments()[0];

            if (type == typeof(int))
                property.SetValue(dataModel, Convert.ToInt32(value));
            else if (type == typeof(long))
                property.SetValue(dataModel, Convert.ToInt64(value));
            else if (type == typeof(float))
                property.SetValue(dataModel, Convert.ToSingle(value));
            else if (type == typeof(double))
                property.SetValue(dataModel, Convert.ToDouble(value));
            else if (type == typeof(decimal))
                property.SetValue(dataModel, Convert.ToDecimal(value));
            else if (type == typeof(bool))
                property.SetValue(dataModel, Convert.ToBoolean(value));
            else if (type == typeof(string))
                property.SetValue(dataModel, Convert.ToString(value));
            else if (type == typeof(DateTime))
                property.SetValue(dataModel, Convert.ToDateTime(value));
            else if (type == typeof(byte[]))
                property.SetValue(dataModel, (byte[])value);
            else
                property.SetValue(dataModel, reader[property.GetColumnType().Name]);
        }

        return dataModel;
    }
}