# A6ToolKits.Layout - A6 工具箱 布局模块

## 如何使用

### 0 添加引用

1. nuget 安装
2. 源码引用

### 1 导入模块

当您导入 `A6ToolKits.Layout` 模块后，您首先应该修改输出目录中的 config.xml 文件，在 `<Modules>` 节点中添加以下内容，以方便应用程序在启动时
加载模块：

```xml
<Module Name="A6ToolKits.Layout" Assembly="A6ToolKits.Layout.dll" Target="A6ToolKits.Layout.LayoutModule"/>
```

### 2 添加基本配置

在配置文件 `config.xml` 的 `<Configuration>` 节点中，你需要添加一些基本配置，以便模块能够生成您所期望的窗口：

```xml
<Window>
    <Title>Application</Title>
    <WindowBorderStyle>Default</WindowBorderStyle>
    <Height>600</Height>
    <Width>800</Width>
    <PrimaryColor>#A9DC76</PrimaryColor>
    <Icon>/Path/To/Icon.png</Icon>
</Window>
```