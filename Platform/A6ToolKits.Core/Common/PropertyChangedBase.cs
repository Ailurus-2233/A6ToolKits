using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace A6ToolKits.Common;

/// <summary>
///     属性变更基类
/// </summary>
public class PropertyChangedBase : INotifyPropertyChanged
{
    /// <summary>
    ///     属性变更事件
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    ///     属性变更通知
    /// </summary>
    /// <param name="propertyName">
    ///     属性名称
    /// </param>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    ///     设置字段值
    /// </summary>
    /// <param name="field">
    ///     目标字段
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