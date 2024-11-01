using System;
using A6ToolKits.Commands.Interfaces;
using A6ToolKits.Common.Attributes.Layout;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;

namespace A6ToolKits.Commands;

/// <summary>
///     Command 对应控件生成器
/// </summary>
public static class CommandControlGenerator
{
    #region MenuItem

    /// <summary>
    ///     基于 ICommandHandler 生成一个菜单项，用于显示在菜单中
    /// </summary>
    /// <param name="commandHandler">
    ///     Command 处理器
    /// </param>
    /// <typeparam name="T">
    ///     Command 定义类型
    /// </typeparam>
    /// <returns>
    ///     生成的菜单项
    /// </returns>
    public static MenuItem GenerateMenuItem<T>(this ICommandHandler commandHandler)
        where T : CommandDefinitionBase, new()
    {
        return commandHandler.GenerateMenuItem(typeof(T));
    }

    /// <summary>
    ///     基于 ActionBase 生成一个菜单项，用于显示在菜单中
    /// </summary>
    /// <param name="commandHandler">
    ///     Command 处理器
    /// </param>
    /// <param name="commandDefinition">
    ///     Command 定义类型，必须是继承 CommandDefinitionBase 的类型
    /// </param>
    /// <returns>
    ///     生成的菜单项
    /// </returns>
    public static MenuItem GenerateMenuItem(this ICommandHandler commandHandler, Type commandDefinition)
    {
        if (CoreService.Instance.Creator?.Create(commandDefinition) is not CommandDefinitionBase definition)
            return new MenuItem();
        var commandItem = new CommandControlItem(definition);
        var nameBinding = new Binding(nameof(commandItem.Name))
        {
            Source = commandItem
        };
        var iconBinding = new Binding(nameof(commandItem.Image))
        {
            Source = commandItem
        };
        var enableBinding = new Binding(nameof(commandItem.Enable))
        {
            Source = commandItem
        };
        var toolTipBinding = new Binding(nameof(commandItem.ToolTip))
        {
            Source = commandItem
        };
        var visibilityBinding = new Binding(nameof(commandItem.Visible))
        {
            Source = commandItem
        };

        var menuItem = new MenuItem();

        menuItem.Bind(MenuItem.HeaderProperty, nameBinding);
        menuItem.Bind(MenuItem.IsEnabledProperty, enableBinding);
        menuItem.Bind(MenuItem.IconProperty, iconBinding);
        menuItem.Bind(MenuItem.IsVisibleProperty, visibilityBinding);

        var toolTip = new ToolTip();
        toolTip.Bind(ToolTip.ContentProperty, toolTipBinding);
        ToolTip.SetTip(menuItem, toolTip);

        menuItem.Click += (_, _) => commandHandler.Run(commandItem);
        return menuItem;
    }

    #endregion

    #region Button

    /// <summary>
    ///     基于 ICommandHandler 生成按钮
    /// </summary>
    /// <param name="commandHandler">
    ///     Command 处理器
    /// </param>
    /// <param name="type">
    ///     生成按钮的类型
    /// </param>
    /// <typeparam name="T">
    ///     Command 定义类型
    /// </typeparam>
    /// <returns>
    ///     生成的按钮控件
    /// </returns>
    public static Button GenerateButton<T>(this ICommandHandler commandHandler, ButtonType type)
        where T : CommandDefinitionBase, new()
    {
        return commandHandler.GenerateButton(typeof(T), type);
    }

    /// <summary>
    ///     基于 ActionBase 生成一个按钮，用于显示在工具栏中
    /// </summary>
    /// <param name="commandHandler">
    ///     Command 处理器
    /// </param>
    /// <param name="commandDefinition">
    ///     Command 定义类型，必须是继承 CommandDefinitionBase 的类型
    /// </param>
    /// <param name="type">
    ///     按钮类型
    /// </param>
    /// <returns>
    ///     生成的按钮
    /// </returns>
    public static Button GenerateButton(this ICommandHandler commandHandler, Type commandDefinition, ButtonType type)
    {
        if (CoreService.Instance.Creator?.Create(commandDefinition) is not CommandDefinitionBase definition)
            return new Button();
        var commandItem = new CommandControlItem(definition);
        var enableBinding = new Binding(nameof(commandItem.Enable))
        {
            Source = commandItem
        };
        var visibilityBinding = new Binding(nameof(commandItem.Visible))
        {
            Source = commandItem
        };
        var toolTipBinding = new Binding(nameof(commandItem.ToolTip))
        {
            Source = commandItem
        };

        var button = new Button();
        button.Click += (_, _) => commandHandler.Run(commandItem);

        button.Bind(Button.IsEnabledProperty, enableBinding);
        button.Bind(Button.IsVisibleProperty, visibilityBinding);

        var toolTip = new ToolTip();
        toolTip.Bind(ToolTip.ContentProperty, toolTipBinding);
        ToolTip.SetTip(button, toolTip);

        button.SetTemplate(commandItem, type);

        return button;
    }

    private static void SetTemplate(this Button button, CommandControlItem commandItem, ButtonType type)
    {
        var nameBinding = new Binding(nameof(commandItem.Name))
        {
            Source = commandItem
        };
        var iconBinding = new Binding(nameof(commandItem.Image))
        {
            Source = commandItem
        };

        button.VerticalAlignment = VerticalAlignment.Center;
        button.HorizontalAlignment = HorizontalAlignment.Center;
        button.HorizontalContentAlignment = HorizontalAlignment.Stretch;
        button.VerticalContentAlignment = VerticalAlignment.Stretch;
        button.Padding = new Thickness(0);

        var image = new Image
        {
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center
        };
        image.Bind(Image.SourceProperty, iconBinding);

        var textBlock = new TextBlock
        {
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center
        };
        textBlock.Bind(TextBlock.TextProperty, nameBinding);

        switch (type)
        {
            case ButtonType.Icon:
                button.Content = image;
                button.PropertyChanged += (_, args) =>
                {
                    if (args.Property.Name is "Bounds")
                        button.SetButtonSize(image);
                };
                break;
            case ButtonType.IconAndText:
                var stackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };
                stackPanel.Children.Add(image);
                stackPanel.Children.Add(textBlock);
                button.Content = stackPanel;
                button.PropertyChanged += (_, args) =>
                {
                    if (args.Property.Name is "Bounds")
                        button.SetButtonSize(image);
                };
                break;
            case ButtonType.Text:
            default:
                button.Content = textBlock;
                break;
        }
    }

    private static void SetButtonSize(this Button button, Image image)
    {
        var bounds = button.Bounds;
        button.Width = bounds.Height;
        button.Height = bounds.Height;
        image.Width = bounds.Height - 2;
        image.Height = bounds.Height - 2;
    }

    #endregion
}