using Avalonia.Controls;
using Avalonia.Media;

namespace A6ToolKits.UIPackage.Controls.Layout.Page.Models;

public class PageItem : ControlBase
{
    private string? _title;
    private string? _toolTip;
    private IImage? _icon;
    private bool _isActivate;
    private ContentControl _content;
    private TabItem _tab;

    public static event EventHandler? Selected;
    public static event EventHandler? Deleted;

    public string? Title
    {
        get => _title;
        set => SetField(ref _title, value);
    }

    public string? ToolTip
    {
        get => _toolTip;
        set => SetField(ref _toolTip, value);
    }

    public IImage? Icon
    {
        get => _icon;
        set => SetField(ref _icon, value);
    }

    public bool IsActivate
    {
        get => _isActivate;
        set => SetField(ref _isActivate, value);
    }

    public ContentControl Content
    {
        get => _content;
        set => SetField(ref _content, value);
    }

    public TabItem Tab
    {
        get => _tab;
        set => SetField(ref _tab, value);
    }

    public void SelectTabItem()
    {
        Selected?.Invoke(this, EventArgs.Empty);
    }

    public void DeleteTabItem()
    {
        Deleted?.Invoke(this, EventArgs.Empty);
    }
}