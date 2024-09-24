using A6ToolKits.Action;
using Avalonia.Controls.Primitives;

namespace A6ToolKits.Layout.Helper.Interfaces;

/// <summary>
///     控件生成器接口，基于 ActionBase 生成控件
/// </summary>
/// <typeparam name="T">
///     控件类型
/// </typeparam>
public interface IControlGenerateHelper<out T> where T : TemplatedControl
{
    /// <summary>
    ///     基于 ActionBase 生成控件
    /// </summary>
    /// <param name="action">
    ///     控件执行的动作
    /// </param>
    /// <returns>
    ///     生成的控件
    /// </returns>
    public T GenerateControl(ActionBase action);
}