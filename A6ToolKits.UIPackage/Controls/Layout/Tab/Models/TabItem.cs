using Avalonia.Controls;
using Avalonia.Media;

namespace A6ToolKits.UIPackage.Controls.Layout.Tab.Models;

public class TabItem : ControlBase
{
    private string? _header;
    private IImage? _icon;
    private string? _toolTip;
    private bool? _isSelected;
    private ContentControl? _content;
    private string? _groupName;

    public static event EventHandler<string>? Selected; 
    public static event EventHandler<string>? Deleted;

    public string? Header
    {
        get => _header;
        set => SetField(ref _header, value);
    }

    public IImage? Icon
    {
        get => _icon;
        set => SetField(ref _icon, value);
    }

    public string? ToolTip
    {
        get => _toolTip;
        set => SetField(ref _toolTip, value);
    }

    public bool? IsSelected
    {
        get => _isSelected;
        set => SetField(ref _isSelected, value);
    }

    public ContentControl Content
    {
        get => _content;
        set => SetField(ref _content, value);
    }
    
    public string? GroupName
    {
        get => _groupName;
        internal set => SetField(ref _groupName, value);
    }

    public void SelectTabItem()
    {
        if (GroupName != null) 
            Selected?.Invoke(this, GroupName);
    }
    
    public void DeleteTabItem()
    {
        if (GroupName != null) 
            Deleted?.Invoke(this, GroupName);
    }
}