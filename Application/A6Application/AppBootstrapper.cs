using System;
using System.Collections.Generic;
using A6ToolKits.Bootstrapper;
using Avalonia;
using Avalonia.Controls;

namespace A6Application;

/// <summary>
///     启动类
/// </summary>
public class AppBootstrapper : BaseBootstrapper<Application, Window>
{
    /// <summary>
    ///     需要加载的模块
    /// </summary>
    /// <returns>
    ///     模块的类型列表
    /// </returns>
    public override List<Type> LoadModules() => [];
}