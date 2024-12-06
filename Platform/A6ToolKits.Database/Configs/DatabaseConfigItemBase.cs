using System.Xml;
using A6ToolKits.Configuration;
using A6ToolKits.Configuration.Exceptions;
using A6ToolKits.Database.Managers;

namespace A6ToolKits.Database.Configs;

/// <summary>
///     数据库配置项基类
/// </summary>
public abstract class DatabaseConfigItemBase : ConfigItemBase
{
    /// <summary>
    ///     数据库名称
    /// </summary>
    public abstract string Name { get; set; }

    /// <summary>
    ///    根据配置构建数据库管理器
    /// </summary>
    /// <returns>
    ///     数据库管理器
    /// </returns>
    public abstract ManagerBase GenerateManager();
}