﻿using Avalonia;
using Avalonia.Styling;
using Avalonia.Themes.Fluent;

namespace A6ToolKits;

/// <summary>
///     基本的应用程序启动类
/// </summary>
public class BaseApp : Application
{
    /// <summary>
    ///     初始化应用程序
    /// </summary>
    public override void Initialize()
    {
        Styles.Add(new FluentTheme());
    }
}