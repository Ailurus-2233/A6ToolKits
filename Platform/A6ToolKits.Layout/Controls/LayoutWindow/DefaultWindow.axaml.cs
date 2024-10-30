﻿using A6ToolKits.Layout.Generator;
using Avalonia.Controls;
using Avalonia.Media;

namespace A6ToolKits.Layout.Controls.LayoutWindow;

/// <summary>
///     默认窗口结构
/// </summary>
public partial class DefaultWindow : Window
{
    /// <summary>
    ///     构造函数
    /// </summary>
    public DefaultWindow()
    {
        InitializeComponent();

        if (ToolBar.ToolBarPanel.Height == 0)
            ToolBarSeparator.Height = 0;

        if (StatusBar.StatusBarPanel.Height == 0)
            StatusBarSeparator.Height = 0;

        WindowBorder.Background = new SolidColorBrush(WindowConfig.Instance.BackgroundColor);
    }
}