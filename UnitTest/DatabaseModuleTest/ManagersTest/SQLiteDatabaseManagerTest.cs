using System.Linq.Expressions;
using A6ToolKits.Database.Configs;
using A6ToolKits.Database.Managers;
using DatabaseModuleTest.DataModels;

namespace DatabaseModuleTest.ManagersTest;

public class SQLiteDatabaseManagerTest : DatabaseManagerTestBase
{
    private SQLiteDatabaseManager? _databaseManager;
    
    protected override DatabaseManagerBase? DatabaseManager => _databaseManager;

    public override void Initialize()
    {
        var config = new SQLiteConfigItem { DataSource = "test.db" };
        _databaseManager = new SQLiteDatabaseManager(config);
        DatabaseManager?.Clear<SQLTestModel>();
    }
}