using A6ToolKits.Container.Attributes;
using A6ToolKits.Database.Configs;
using A6ToolKits.Database.Exceptions;
using A6ToolKits.Database.Managers;
using A6ToolKits.Module;
using A6ToolKits.Module.Exceptions;

// ReSharper disable All

namespace A6ToolKits.Database;

/// <summary>
///     数据库模块
/// </summary>
[AutoRegister(typeof(IDatabaseModule), RegisterType.Singleton)]
public class DatabaseModule : ModuleBase<DatabaseConfigItem>, IDatabaseModule
{
    /// <summary>
    ///     数据库管理器列表
    /// </summary>
    public List<ManagerBase> Managers { get; } = [];

    /// <summary>
    ///     初始化数据库模块
    /// </summary>
    public override void Initialize()
    {
        Managers.Clear();

        foreach (var config in Config.Children)
        {
            if (config is not DatabaseConfigItemBase dbConfig)
                throw new LoadModuleException(nameof(DatabaseModule), $"Load database config failed: {config}");
            Managers.Add(dbConfig.GenerateManager());
        }
    }

    /// <inheritdoc />
    public IManager GetDatabaseManger(string id)
    {
        var manager = Managers.FirstOrDefault(manager => manager.ManagerId == id);
        if (manager == null)
            throw new ManagerNotExistException(id);
        return manager;
    }

    /// <inheritdoc />
    public T GetDatabaseManager<T>(string id) where T : IManager
    {
        return (T)GetDatabaseManger(id);
    }
}