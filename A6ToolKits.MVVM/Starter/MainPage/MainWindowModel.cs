using A6ToolKits.Common;
using A6ToolKits.Common.Attributes;
using A6ToolKits.MVVM.Common;

namespace A6ToolKits.MVVM.Starter.MainPage;

[AutoRegister]
public class MainWindowModel : ViewModelBase
{
    private string? _title = "Windows";
    private int? _width = 800;
    private int? _height = 450;

    public string? Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }

    public int? Width
    {
        get => _width;
        set => SetProperty(ref _width, value);
    }

    public int? Height { 
        get => _height;
        set => SetProperty(ref _height, value);
    }
}