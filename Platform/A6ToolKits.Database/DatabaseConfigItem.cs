﻿using System.Xml;
using A6ToolKits.Attributes;
using A6Toolkits.Configuration;
using A6ToolKits.Database.Configs;

namespace A6ToolKits.Database;

/// <summary>
///     数据库配置项
/// </summary>
[ModuleConfig]
[ConfigName("Database")]
public class DatabaseConfigItem : ConfigItemBase
{
    /// <inheritdoc />
    public override bool IsNecessary => true;

    /// <inheritdoc />
    public override void SetDefault()
    {
        Children.Clear();
        Children.Add(new XMLConfigItem());
        Children.Add(new CSVConfigItem());
        Children.Add(new SQLiteConfigItem());
    }
}