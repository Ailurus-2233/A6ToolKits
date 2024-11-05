using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Styling;

namespace A6ToolKits.Bootstrapper.Interfaces;

/// <summary>
///     一些 Avalonia 应用控制的接口
/// </summary>
public interface IApplicationController
{
    /// <summary>
    ///     应用的启动方法
    /// </summary>
    public void Run(string[] args);

    /// <summary>
    ///     结束应用的方法
    /// </summary>
    public void StopApplication();

    /// <summary>
    ///     获得应用主窗口
    /// </summary>
    /// <returns></returns>
    public Window GetMainWindow();

    /// <summary>
    ///     设置应用的主窗口
    /// </summary>
    /// <param name="mainWindow">
    ///     主窗口
    /// </param>
    public void SetupMainWindow(Window mainWindow);

    /// <summary>
    ///     设置主应用的主题
    /// </summary>
    /// <param name="theme">
    ///     主题
    /// </param>
    public void SetupTheme(ThemeVariant theme);

    /// <summary>
    ///     获取需要加载的模块
    /// </summary>
    /// <returns></returns>
    public List<Type> LoadModules();
}