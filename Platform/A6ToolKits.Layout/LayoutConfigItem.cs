﻿using System.Reflection;
using System.Xml;
using A6ToolKits.Common.Exceptions;
using A6ToolKits.Config;
using A6ToolKits.Helper.Configurator;
using A6ToolKits.Layout.Generator;
using A6ToolKits.Layout.Generator.Enums;
using Avalonia.Media;
using Avalonia.Styling;

namespace A6ToolKits.Layout;

/// <summary>
///     布局配置项
/// </summary>
public class LayoutConfigItem : ConfigItemBase
{
    /// <summary>
    ///     窗口标题
    /// </summary>
    public string Title { get; set; } = "";

    /// <summary>
    ///     窗口边框样式
    /// </summary>
    public string BorderStyle { get; set; } = "";

    /// <summary>
    ///     窗口宽度
    /// </summary>
    public string Width { get; set; } = "";

    /// <summary>
    ///     窗口高度
    /// </summary>
    public string Height { get; set; } = "";

    /// <summary>
    ///     窗口重点色
    /// </summary>
    public string PrimaryColor { get; set; } = "";

    /// <summary>
    ///     窗口背景色
    /// </summary>
    public string BackgroundColor { get; set; } = "";
    
    /// <summary>
    ///     窗口图标
    /// </summary>
    public string Icon { get; set; }
    
    internal void SetToResources()
    {
        var config = WindowConfig.Instance;
        config.Title = !string.IsNullOrEmpty(Title) ? Title : string.Empty;
        config.Width = !string.IsNullOrEmpty(Width) ? double.Parse(Width) : 0;
        config.Height = !string.IsNullOrEmpty(Height) ? double.Parse(Height) : 0;
        config.PrimaryColor =
            Color.TryParse(PrimaryColor, out var primaryColor)
                ? Color.FromRgb(primaryColor.R, primaryColor.G, primaryColor.B)
                : Colors.CornflowerBlue;
        config.BackgroundColor =
            Color.TryParse(BackgroundColor, out var backgroundColor)
                ? Color.FromRgb(backgroundColor.R, backgroundColor.G, backgroundColor.B)
                : Colors.Wheat;
        config.BorderStyle = Enum.TryParse<WindowBorderType>(BorderStyle, out var borderStyle)
            ? borderStyle
            : WindowBorderType.Default;

        var brightness = 0.299 * backgroundColor.R + 0.587 * backgroundColor.G + 0.114 * backgroundColor.B;
        config.Theme = brightness > 128 ? ThemeVariant.Light : ThemeVariant.Dark;
        config.Icon = new Uri(Icon);
    }

    /// <summary>
    ///     从配置文件中加载配置
    /// </summary>
    /// <exception cref="Exception">
    ///     配置文件未找到
    /// </exception>
    public override void LoadConfig()
    {
        var layoutConfigItem = ConfigHelper.GetElements("Window")?.Item(0);
        if (layoutConfigItem == null)
            throw new ConfigLoadException(typeof(LayoutConfigItem));
        GenerateFromXmlNode(layoutConfigItem);
        var runAssembly = Assembly.GetEntryAssembly();
        Icon = runAssembly == null ? "" : $"avares://{runAssembly.FullName.Split(',')[0]}/{Icon}";
    }

    /// <summary>
    ///     创建基本的配置文件节点
    /// </summary>
    /// <returns>
    ///     配置文件节点
    /// </returns>
    public override XmlElement CreateDefaultConfig()
    {
        var doc = ConfigHelper.GetDefaultConfig();
        var result = doc.CreateElement("Window");
        result.SetAttribute("Title", "A6ToolKit-Application");
        result.SetAttribute("BorderStyle", "Default");
        result.SetAttribute("Width", "800");
        result.SetAttribute("Height", "600");
        result.SetAttribute("PrimaryColor", "#6495ED");
        result.SetAttribute("BackgroundColor", "#FFFFFF");
        result.SetAttribute("Icon", "");
        return result;
    }

    /// <summary>
    ///     设置默认值
    /// </summary>
    public override void SetDefault()
    {
        Title = "A6ToolKit-Application";
        BorderStyle = "Default";
        Width = "800";
        Height = "600";
        PrimaryColor = "#6495ED";
        BackgroundColor = "#FFFFFF";
        Icon = "";
    }
}