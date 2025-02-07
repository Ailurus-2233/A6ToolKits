using Avalonia.Controls;
using Avalonia.Styling;
namespace A6ToolKits.ApplicationController;

/// <summary>
///     一些 Avalonia 应用控制的接口
/// </summary>
public interface IApplicationController
{
    /// <summary>
    ///     主窗口的get/set方法
    /// </summary>
    public Window? MainWindow { get; set; }

    /// <summary>
    ///     主题的get/set方法
    /// </summary>
    public ThemeVariant Theme { get; set; }

    /// <summary>
    ///     开始执行应用程序
    /// </summary>
    public void Run(string[] args);

    /// <summary>
    ///     停止应用程序
    /// </summary>
    public void StopApplication();
}