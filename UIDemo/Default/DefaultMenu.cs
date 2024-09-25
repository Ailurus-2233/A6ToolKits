using A6ToolKits.Attributes;
using A6ToolKits.Layout.Definitions;
using UIDemo.Default.Actions;

namespace UIDemo.Default;

public class DefaultMenu : IDefiner
{
    [MenuBar("文件:0")] public NewFile NewFile { get; } = new();
    [MenuBar("文件:0")] public NewFolder NewFolder { get; } = new();
    [MenuBar("文件:1")] public OpenFile OpenFile { get; } = new();
    [MenuBar("文件:1")] public OpenFolder OpenFolder { get; } = new();
    [MenuBar("编辑:0")] public NewFile NewFile1 { get; } = new();
    [MenuBar("编辑:0")] public NewFolder NewFolder1 { get; } = new();
    [MenuBar("编辑:1")] public OpenFile OpenFile1 { get; } = new();
    [MenuBar("编辑:1")] public OpenFolder OpenFolder1 { get; } = new();
    [MenuBar("编辑:0", "编辑:1")] public NewFile NewFile2 { get; } = new();
    [MenuBar("编辑:0", "编辑:1")] public NewFolder NewFolder2 { get; } = new();
    [MenuBar("编辑:1", "AAAA:1")] public OpenFile OpenFile2 { get; } = new();
    [MenuBar("编辑:1", "AAAA:1")] public OpenFolder OpenFolder2 { get; } = new();
    [MenuBar("文件:0")] public NewFile NewFile3 { get; } = new();
    [MenuBar("文件:0")] public NewFolder NewFolder3 { get; } = new();
    [MenuBar("文件:1")] public OpenFile OpenFile3 { get; } = new();
    [MenuBar("文件:1")] public OpenFolder OpenFolder3 { get; } = new();
}