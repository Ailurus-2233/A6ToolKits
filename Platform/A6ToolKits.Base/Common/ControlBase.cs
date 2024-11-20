using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Controls.Primitives;

namespace A6ToolKits.Common;

/// <summary>
///     控件基类，实现了INotifyPropertyChanged接口
/// </summary>
public abstract class ControlBase : TemplatedControl, INotifyPropertyChanged
{
    public new event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}