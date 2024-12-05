using System.Xml;
using A6ToolKits.Configuration;
using A6ToolKits.Configuration.Exceptions;

namespace A6ToolKits.Database.Configs;

/// <summary>
///     数据库配置项基类
/// </summary>
public abstract class DatabaseConfigItemBase : ConfigItemBase
{
    /// <summary>
    ///     数据库名称
    /// </summary>
    public abstract string DatabaseName { get; }
}