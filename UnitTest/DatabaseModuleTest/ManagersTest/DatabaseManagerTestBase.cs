using A6ToolKits.Database.Managers;
using DatabaseModuleTest.DataModels;

namespace DatabaseModuleTest.ManagersTest;

public abstract class DatabaseManagerTestBase
{
    protected abstract DatabaseManagerBase DatabaseManager { get; }
    
}