using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace A6ToolKits.MVVM.Common;

/// <summary>
/// ViewModel基类，继承自ReactiveUI.ReactiveObject
/// 用法：继承ViewModelBase，然后在ViewModel中使用ReactiveObject的属性
/// </summary>
public partial class ViewModelBase : ObservableObject
{
}