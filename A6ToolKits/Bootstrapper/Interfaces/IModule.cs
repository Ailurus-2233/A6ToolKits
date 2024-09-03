using System.Collections.Generic;

namespace A6ToolKits.Bootstrapper.Interfaces;

/// <summary>
/// 一个模块接口，用于记录模块的信息
/// </summary>
public interface IModule
{
    public string? ModuleName { get; set; }
    public string? ModuleVersion { get; set; }
    public string? ModuleDescription { get; set; }
}