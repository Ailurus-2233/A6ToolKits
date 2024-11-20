using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Controls.Primitives;

namespace A6ToolKits.Common;

/// <summary>
///     控件基类，实现了INotifyPropertyChanged接口
/// </summary>
public abstract class ControlBase : TemplatedControl, INotifyPropertyChanged
{
    /// <summary>
    ///     属性变更事件
    /// </summary>
    public new event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    ///     设置字段值
    /// </summary>
    /// <param name="field">
    ///     字段引用
    /// </param>
    /// <param name="value">
    ///     字段值
    /// </param>
    /// <param name="propertyName">
    ///     属性名称
    /// </param>
    /// <typeparam name="T">
    ///     字段类型
    /// </typeparam>
    /// <returns>
    ///     是否设置成功
    /// </returns>
    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}