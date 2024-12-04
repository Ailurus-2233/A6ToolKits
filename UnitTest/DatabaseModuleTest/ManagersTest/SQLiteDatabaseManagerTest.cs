using A6ToolKits.Database.Configs;
using A6ToolKits.Database.Managers;
using DatabaseModuleTest.DataModels;

namespace DatabaseModuleTest.ManagersTest;

public class SQLiteDatabaseManagerTest : DatabaseManagerTestBase
{
    private SQLiteDatabaseManager _databaseManager;
    
    protected override DatabaseManagerBase DatabaseManager => _databaseManager;

    protected readonly List<SQLTestModel> Data =
    [
        new() { Id = 1, Name = "A", Age = 1 },
        new() { Id = 2, Name = "B", Age = 2 },
        new() { Id = 3, Name = "C", Age = 3 }
    ];
    
    [SetUp]
    public void SetUp()
    {
        var config = new SQLiteConfigItem { DataSource = "test.db" };
        _databaseManager = new SQLiteDatabaseManager(config);
    }

    [Test]
    public void CreateTableTest()
    {
        DatabaseManager.CreateTable(Data[0]);
    }

    [Test]
    public void AddTest()
    {
        DatabaseManager.Add(Data);
    }
}