﻿using A6ToolKits.Attributes;
using A6ToolKits.Layout.Default.Actions;
using A6ToolKits.Layout.Definitions;

namespace A6ToolKits.Layout.Default;
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
public class DefaultButtonBar : IDefinition
{
    [ButtonBar(0, "Left")] public NewFile NewFile { get; } = new();

    [ButtonBar(0, "Left")] public OpenFile OpenFile { get; } = new();

    [ButtonBar(1, "Left")] public NewFolder NewFolder { get; } = new();

    [ButtonBar(1, "Right")] public OpenFolder OpenFolder { get; } = new();

    [ButtonBar(2, "Left")] public PreviewPage PreviewPage { get; } = new();

    [ButtonBar(2, "Left")] public NextPage NextPage { get; } = new();

    [ButtonBar(2, "Left")] public ActiveAboutPage ActiveAboutPage { get; } = new();
}

#pragma warning restore CS1591 // 缺少对公共可见类型或成员的 XML 注释