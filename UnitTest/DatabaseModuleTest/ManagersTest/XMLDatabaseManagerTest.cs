using A6ToolKits.Database.Exceptions;
using A6ToolKits.Database.Managers;
using DatabaseModuleTest.DataModels;

namespace DatabaseModuleTest.ManagersTest;

public class XMLDatabaseManagerTest : FileDatabaseManagerTestBase
{
    protected override FileDatabaseManagerBase DatabaseManager { get; } = new XMLDatabaseManager("test");
}