using A6ToolKits.Database.Managers;
using DatabaseModuleTest.DataModels;

namespace DatabaseModuleTest.ManagersTest;

public class CsvDatabaseManagerTest : FileDatabaseManagerTestBase
{
    protected override FileDatabaseManagerBase DatabaseManager { get; } = new CsvDatabaseManager("test", "test");
}