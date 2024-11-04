using System;
using A6ToolKits.Common.Attributes.Layout;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Layout;

namespace A6ToolKits.Commands.ControlGenerators;

/// <summary>
///     MenuItem 生成器
/// </summary>
public static class MenuItemGenerator
{
    /// <summary>
    ///     基于 ActionBase 生成一个菜单项，用于显示在菜单中
    /// </summary>
    /// <param name="command">
    ///     Command 对象
    /// </param>
    /// <param name="type">
    ///     菜单类型
    /// </param>
    /// <param name="height">
    ///     控件高度
    /// </param>
    /// <returns>
    ///     生成的菜单项
    /// </returns>
    public static MenuItem GenerateMenuItem(this CommandBase command, MenuItemType type, double height)
    {
        var nameBinding = new Binding(nameof(command.Name))
        {
            Source = command
        };
        var iconBinding = new Binding(nameof(command.Image))
        {
            Source = command
        };
        var enableBinding = new Binding(nameof(command.Enable))
        {
            Source = command
        };
        var toolTipBinding = new Binding(nameof(command.ToolTip))
        {
            Source = command
        };
        var visibilityBinding = new Binding(nameof(command.Visible))
        {
            Source = command
        };

        var menuItem = new MenuItem();
        
        menuItem.Bind(InputElement.IsEnabledProperty, enableBinding);
        menuItem.Bind(Visual.IsVisibleProperty, visibilityBinding);

        var grid = new Grid()
        {
            ColumnDefinitions = new ColumnDefinitions( "Auto,*"),
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Center
        };

        var image = new Image();
        image.Bind(Image.SourceProperty, iconBinding);
        image.Margin = new Thickness(height * 0.1);
        Grid.SetColumn(image, 0);
        var textBlock = new TextBlock();
        textBlock.Bind(TextBlock.TextProperty, nameBinding);
        textBlock.Margin = new Thickness(10, 0, 0, 0);
        Grid.SetColumn(textBlock, 1);
        
        switch (type)
        {
            case MenuItemType.Text:
                break;
            case MenuItemType.IconAndText:
                grid.Children.Add(image);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        grid.Children.Add(textBlock);
        menuItem.Header = grid;
        menuItem.Height = height;

        var toolTip = new ToolTip();
        toolTip.Bind(ContentControl.ContentProperty, toolTipBinding);
        ToolTip.SetTip(menuItem, toolTip);
        
        menuItem.Click += (_, _) =>
        {
            command.Run().Wait();
            command.Update();
        };
        return menuItem;
    }
}