using A6ToolKits.Layout.Attributes;
using A6ToolKits.Layout.Default.Actions;
using A6ToolKits.Layout.Definitions;

namespace A6ToolKits.Layout.Default;

public class DefaultMenu : MenuDefinition
{
    [Menu("新建文件夹", "文件:0")] public NewFile NewFile { get; } = new();
    [Menu("新建文件夹", "文件:0")] public NewFolder NewFolder { get; } = new();
    [Menu("打开文件", "文件:1")] public OpenFile OpenFile { get; } = new();
    [Menu("打开文件夹", "文件:1")] public OpenFolder OpenFolder { get; } = new();
    [Menu("新建文件", "编辑:0")] public NewFile NewFile1 { get; } = new();
    [Menu("新建文件夹", "编辑:0")] public NewFolder NewFolder1 { get; } = new();
    [Menu("打开文件", "编辑:1")] public OpenFile OpenFile1 { get; } = new();
    [Menu("打开文件夹", "编辑:1", "编辑:1")] public OpenFolder OpenFolder1 { get; } = new();
    [Menu("新建文件", "编辑:0", "编辑:1")] public NewFile NewFile2 { get; } = new();
    [Menu("新建文件夹", "编辑:0", "AAAA:1")] public NewFolder NewFolder2 { get; } = new();
    [Menu("打开文件", "编辑:1", "AAAA:1")] public OpenFile OpenFile2 { get; } = new();
    [Menu("打开文件夹", "编辑:1", "AAAA:1")] public OpenFolder OpenFolder2 { get; } = new();
}