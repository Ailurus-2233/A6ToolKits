using A6ToolKits.MVVM.Container;
using Avalonia;
using Avalonia.Controls;
using Dock.Model.Adapters;
using Dock.Model.Core;

namespace A6ToolKits.Layout.Controls;

public class DockItem : ContentControl, IDockable
{
    #region IDockable Members

    private readonly TrackingAdapter _trackingAdapter = new();
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public object? Context { get; set; }
    public IDockable? Owner { get; set; }
    public IDockable? OriginalOwner { get; set; }
    public IFactory? Factory { get; set; }
    public bool CanClose { get; set; } = true;
    public bool CanPin { get; set; } = true;
    public bool CanFloat { get; set; } = true;

    public bool OnClose()
    {
        return true;
    }

    public void OnSelected()
    {
    }

    public void GetVisibleBounds(out double x, out double y, out double width, out double height)
    {
        _trackingAdapter.GetVisibleBounds(out x, out y, out width, out height);
    }

    public void SetVisibleBounds(double x, double y, double width, double height)
    {
        _trackingAdapter.SetVisibleBounds(x, y, width, height);
        OnVisibleBoundsChanged(x, y, width, height);
    }

    public void OnVisibleBoundsChanged(double x, double y, double width, double height)
    {
    }

    public void GetPinnedBounds(out double x, out double y, out double width, out double height)
    {
        _trackingAdapter.GetPinnedBounds(out x, out y, out width, out height);
    }

    public void SetPinnedBounds(double x, double y, double width, double height)
    {
        _trackingAdapter.SetPinnedBounds(x, y, width, height);
        OnPinnedBoundsChanged(x, y, width, height);
    }

    public void OnPinnedBoundsChanged(double x, double y, double width, double height)
    {
    }

    public void GetTabBounds(out double x, out double y, out double width, out double height)
    {
        _trackingAdapter.GetTabBounds(out x, out y, out width, out height);
    }

    public void SetTabBounds(double x, double y, double width, double height)
    {
        _trackingAdapter.SetTabBounds(x, y, width, height);
        OnTabBoundsChanged(x, y, width, height);
    }

    public void OnTabBoundsChanged(double x, double y, double width, double height)
    {
    }

    public void GetPointerPosition(out double x, out double y)
    {
        _trackingAdapter.GetPointerPosition(out x, out y);
    }

    public void SetPointerPosition(double x, double y)
    {
        _trackingAdapter.SetPointerPosition(x, y);
        OnPointerPositionChanged(x, y);
    }

    public void OnPointerPositionChanged(double x, double y)
    {
    }

    public void GetPointerScreenPosition(out double x, out double y)
    {
        _trackingAdapter.GetPointerScreenPosition(out x, out y);
    }

    public void SetPointerScreenPosition(double x, double y)
    {
        _trackingAdapter.SetPointerScreenPosition(x, y);
        OnPointerScreenPositionChanged(x, y);
    }

    public void OnPointerScreenPositionChanged(double x, double y)
    {
    }

    #endregion
}