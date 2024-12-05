using A6ToolKits.Container.Attributes;
using A6ToolKits.Module;

namespace A6ToolKits.Database;

/// <summary>
///     数据库模块
/// </summary>
[AutoRegister(typeof(IDatabaseModule), RegisterType.Singleton)]
public class DatabaseModule: ModuleBase<DatabaseConfigItem>, IDatabaseModule
{
    /// <summary>
    ///     初始化数据库模块
    /// </summary>
    public override void Initialize()
    {
        
    }
}