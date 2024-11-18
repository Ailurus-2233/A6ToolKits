using System.Reflection;
using System.Runtime.CompilerServices;
using A6ToolKits.Common.Container;
using A6ToolKits.MVVM.Attributes;
using A6ToolKits.MVVM.Exceptions;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace A6ToolKits.MVVM;

/// <summary>
///     属性变更基类
/// </summary>
public class ViewModelBase : ObservableObject
{
    protected Control GetView()
    {
        var targetType = GetType();
        var targetViewAttribute = targetType.GetCustomAttribute<TargetViewAttribute>();
        if (targetViewAttribute == null) throw new NotHaveTargetViewProperty(GetType());
        var view = IoC.GetInstance(targetViewAttribute.ViewType);
        return view as Control ?? throw new CanNotGetTargetViewException(targetViewAttribute.ViewType);
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