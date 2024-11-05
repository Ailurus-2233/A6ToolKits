using A6ToolKits.Config;

namespace A6ToolKits.AssemblyPath;

/// <summary>
///     程序集配置项
/// </summary>
public class AssemblyPathConfigItem : ConfigItemBase
{
    /// <summary>
    ///     程序集路径
    /// </summary>
    public string? Path { get; set; }
}