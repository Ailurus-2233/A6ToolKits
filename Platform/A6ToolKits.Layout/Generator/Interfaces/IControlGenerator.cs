using Avalonia.Controls;
namespace A6ToolKits.Layout.ControlGenerator.Interfaces;

/// <summary>
///     指定控件的生成器
/// </summary>
public interface IControlGenerator<out T> where T : Control
{
    /// <summary>
    ///     生成控件的方法
    /// </summary>
    /// <returns>
    ///     生成的控件
    /// </returns>
    public T? Generate();
}