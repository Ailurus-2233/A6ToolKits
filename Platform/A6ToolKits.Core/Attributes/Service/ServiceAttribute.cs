using System;

namespace A6ToolKits.Attributes.Service;

/// <summary>
///     服务类的属性，加入该属性后，会在启动时候被整理到全局服务管理器中，通过服务名称获取对应的实例
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ServiceAttribute(string name): Attribute
{
    /// <summary>
    ///     服务名称
    /// </summary>
    public string ServiceName { get; set; } = name;
}