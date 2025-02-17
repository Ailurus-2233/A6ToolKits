﻿# A6ToolKits.Layout - A6 工具箱 布局模块

该模块提供一种快速生成应用程序窗口的方案，通过配置文件的方式，可以快速生成一个带有菜单栏、工具栏、状态栏的窗口，同时提供了一种自定义的窗口样式。

> **注**:
> 该模块是学习 Avalonia 的一个练手项目，样式和功能不是很完善，仅供学习参考

## 如何使用

### 0 添加引用

1. nuget 安装
2. 源码引用

### 1 导入模块

当您导入 `A6ToolKits.Layout` 模块后，您需要在您定义的 `Bootstrapper` 中增加以下内容，用于在应用程序中启动这个模块：

```csharp
protected override List<Type> ToLoadModules =>
[
    typeof(ILayoutModule),
];
```

### 2 添加基本配置

在配置文件 `config.xml` 的 `<Configuration>` 节点中，你需要添加一些基本配置，以便模块能够生成您所期望的窗口：

```xml
<Window Title="UIDemo" BorderStyle="Origin" Width="800" Height="600" PrimaryColor="#A6E3A1" BackgroundColor="#FFFFFF" Icon="Assert/Icon.png" />
```

其中值得注意的是 `BorderStyle`，这个有三种值可以填入，`Default`、`Origin`、`None`:

1. `Default` 是这个包定义的一种窗体结构，他去掉了 Avalonia 原生的窗口栏，并以包中 `HeaderBar` 控件来代替
2. `Origin` 是原生 Avalonia 的窗口栏
3. `None` 则是完全没有 HeaderBar，窗口中只有一个 `MainRegion` 对象交给开发者来编辑其中的内容

然后 `PrimaryColor` 影响的是窗口中一些控件展示的颜色，`BackgroundColor` 则是影响的整个窗体基本的背景颜色

### 3 获取 Window 对象

当然如果您对自动生成的窗口有自己的想法，需要对他进行微调，你可以通过如下代码来获取当前的窗体对象， `IoC.Get<IWindowController>().GetMainWindow`。

> 注：自动生成的 Window 会自动赋值给 Bootstrapper 中的 MainWindow

## 自动生成控件

### 菜单栏

### 工具栏

### 状态栏

### 页面布局

## 展示