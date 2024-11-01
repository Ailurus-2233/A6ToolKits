﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace A6ToolKits.MVVM;

/// <summary>
///     ViewModel基类，继承自ReactiveUI.ReactiveObject
///     用法：继承ViewModelBase，然后在ViewModel中使用ReactiveObject的属性
/// </summary>
public abstract class ViewModelBase : ObservableObject
{
}