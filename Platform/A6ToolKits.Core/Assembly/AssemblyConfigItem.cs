﻿using A6ToolKits.Config;

namespace A6ToolKits.Assembly;

/// <summary>
///     程序集配置项
/// </summary>
public class AssemblyConfigItem : ConfigItemBase
{
    /// <summary>
    ///     程序集路径
    /// </summary>
    public string? Path { get; set; }
}