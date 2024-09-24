﻿using Avalonia.Controls;

namespace A6ToolKits.Layout.Controls;

public partial class HeaderBar : UserControl
{
    public HeaderBar()
    {
        InitializeComponent();
    }

    public void SetMenuVisible(bool visible)
    {
        Menu.IsVisible = visible;
        MenuSeparator.IsVisible = visible;
    }

    public void SetButtonBarVisible(bool visible)
    {
        ButtonBar.IsVisible = visible;
        HeaderSeparator.IsVisible = visible || RightButtonBar.IsVisible;
    }

    public void SetRightButtonBarVisible(bool visible)
    {
        RightButtonBar.IsVisible = visible;
        HeaderSeparator.IsVisible = visible || ButtonBar.IsVisible;
    }
}