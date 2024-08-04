﻿using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Avalonia;
using Avalonia.Controls;
using Dock.Model.Adapters;
using Dock.Model.Avalonia.Core;
using Dock.Model.Core;

namespace A6ToolKits.Layout.Controls.Core;

/// <summary>
/// 
/// </summary>
public class DockItemBase : ContentControl, IDockable
{
        /// <summary>
    /// Defines the <see cref="Id"/> property.
    /// </summary>
    public static readonly DirectProperty<DockItemBase, string> IdProperty =
        AvaloniaProperty.RegisterDirect<DockItemBase, string>(nameof(Id), o => o.Id, (o, v) => o.Id = v);

    /// <summary>
    /// Defines the <see cref="Title"/> property.
    /// </summary>
    public static readonly DirectProperty<DockItemBase, string> TitleProperty =
        AvaloniaProperty.RegisterDirect<DockItemBase, string>(nameof(Title), o => o.Title, (o, v) => o.Title = v);

    /// <summary>
    /// Defines the <see cref="Context"/> property.
    /// </summary>
    public static readonly DirectProperty<DockItemBase, object?> ContextProperty =
        AvaloniaProperty.RegisterDirect<DockItemBase, object?>(nameof(Context), o => o.Context, (o, v) => o.Context = v);

    /// <summary>
    /// Defines the <see cref="Owner"/> property.
    /// </summary>
    public static readonly DirectProperty<DockItemBase, IDockable?> OwnerProperty =
        AvaloniaProperty.RegisterDirect<DockItemBase, IDockable?>(nameof(Owner), o => o.Owner, (o, v) => o.Owner = v);

    /// <summary>
    /// Defines the <see cref="OriginalOwner"/> property.
    /// </summary>
    public static readonly DirectProperty<DockItemBase, IDockable?> OriginalOwnerProperty =
        AvaloniaProperty.RegisterDirect<DockItemBase, IDockable?>(nameof(OriginalOwner), o => o.OriginalOwner, (o, v) => o.OriginalOwner = v);

    /// <summary>
    /// Defines the <see cref="Factory"/> property.
    /// </summary>
    public static readonly DirectProperty<DockItemBase, IFactory?> FactoryProperty =
        AvaloniaProperty.RegisterDirect<DockItemBase, IFactory?>(nameof(Factory), o => o.Factory, (o, v) => o.Factory = v);

    /// <summary>
    /// Defines the <see cref="CanClose"/> property.
    /// </summary>
    public static readonly DirectProperty<DockItemBase, bool> CanCloseProperty =
        AvaloniaProperty.RegisterDirect<DockItemBase, bool>(nameof(CanClose), o => o.CanClose, (o, v) => o.CanClose = v);

    /// <summary>
    /// Defines the <see cref="CanPin"/> property.
    /// </summary>
    public static readonly DirectProperty<DockItemBase, bool> CanPinProperty =
        AvaloniaProperty.RegisterDirect<DockItemBase, bool>(nameof(CanPin), o => o.CanPin, (o, v) => o.CanPin = v);

    /// <summary>
    /// Defines the <see cref="CanFloat"/> property.
    /// </summary>
    public static readonly DirectProperty<DockItemBase, bool> CanFloatProperty =
        AvaloniaProperty.RegisterDirect<DockItemBase, bool>(nameof(CanFloat), o => o.CanFloat, (o, v) => o.CanFloat = v);

    private readonly TrackingAdapter _trackingAdapter;
    private string _id = string.Empty;
    private string _title = string.Empty;
    private object? _context;
    private IDockable? _owner;
    private IDockable? _originalOwner;
    private IFactory? _factory;
    private bool _canClose = true;
    private bool _canPin = true;
    private bool _canFloat = true;

    /// <summary>
    /// Initializes new instance of the <see cref="DockableBase"/> class.
    /// </summary>
    protected DockItemBase()
    {
        _trackingAdapter = new TrackingAdapter();
    }

    /// <inheritdoc/>
    [DataMember(IsRequired = false, EmitDefaultValue = true)]
    [JsonPropertyName("Id")]
    public string Id
    {
        get => _id;
        set => SetAndRaise(IdProperty, ref _id, value);
    }

    /// <inheritdoc/>
    [DataMember(IsRequired = false, EmitDefaultValue = true)]
    [JsonPropertyName("Title")]
    public string Title
    {
        get => _title;
        set => SetAndRaise(TitleProperty, ref _title, value);
    }

    /// <inheritdoc/>
    [DataMember(IsRequired = false, EmitDefaultValue = true)]
    [JsonPropertyName("Context")]
    public object? Context
    {
        get => _context;
        set => SetAndRaise(ContextProperty, ref _context, value);
    }

    /// <inheritdoc/>
    [ResolveByName]
    [IgnoreDataMember]
    [JsonIgnore]
    public IDockable? Owner
    {
        get => _owner;
        set => SetAndRaise(OwnerProperty, ref _owner, value);
    }

    /// <inheritdoc/>
    [ResolveByName]
    [IgnoreDataMember]
    [JsonIgnore]
    public IDockable? OriginalOwner
    {
        get => _originalOwner;
        set => SetAndRaise(OriginalOwnerProperty, ref _originalOwner, value);
    }

    /// <inheritdoc/>
    [IgnoreDataMember]
    [JsonIgnore]
    public IFactory? Factory
    {
        get => _factory;
        set => SetAndRaise(FactoryProperty, ref _factory, value);
    }

    /// <inheritdoc/>
    [DataMember(IsRequired = false, EmitDefaultValue = true)]
    [JsonPropertyName("CanClose")]
    public bool CanClose
    {
        get => _canClose;
        set => SetAndRaise(CanCloseProperty, ref _canClose, value);
    }

    /// <inheritdoc/>
    [DataMember(IsRequired = false, EmitDefaultValue = true)]
    [JsonPropertyName("CanPin")]
    public bool CanPin
    {
        get => _canPin;
        set => SetAndRaise(CanPinProperty, ref _canPin, value);
    }

    /// <inheritdoc/>
    [DataMember(IsRequired = false, EmitDefaultValue = true)]
    [JsonPropertyName("CanFloat")]
    public bool CanFloat
    {
        get => _canFloat;
        set => SetAndRaise(CanFloatProperty, ref _canFloat, value);
    }

    /// <inheritdoc/>
    public virtual bool OnClose()
    {
        return true;
    }

    /// <inheritdoc/>
    public virtual void OnSelected()
    {
    }

    /// <inheritdoc/>
    public void GetVisibleBounds(out double x, out double y, out double width, out double height)
    {
        _trackingAdapter.GetVisibleBounds(out x, out y, out width, out height);
    }

    /// <inheritdoc/>
    public void SetVisibleBounds(double x, double y, double width, double height)
    {
        _trackingAdapter.SetVisibleBounds(x, y, width, height);
        OnVisibleBoundsChanged(x, y, width, height);
    }

    /// <inheritdoc/>
    public virtual void OnVisibleBoundsChanged(double x, double y, double width, double height)
    {
    }

    /// <inheritdoc/>
    public void GetPinnedBounds(out double x, out double y, out double width, out double height)
    {
        _trackingAdapter.GetPinnedBounds(out x, out y, out width, out height);
    }

    /// <inheritdoc/>
    public void SetPinnedBounds(double x, double y, double width, double height)
    {
        _trackingAdapter.SetPinnedBounds(x, y, width, height);
        OnPinnedBoundsChanged(x, y, width, height);
    }

    /// <inheritdoc/>
    public virtual void OnPinnedBoundsChanged(double x, double y, double width, double height)
    {
    }

    /// <inheritdoc/>
    public void GetTabBounds(out double x, out double y, out double width, out double height)
    {
        _trackingAdapter.GetTabBounds(out x, out y, out width, out height);
    }

    /// <inheritdoc/>
    public void SetTabBounds(double x, double y, double width, double height)
    {
        _trackingAdapter.SetTabBounds(x, y, width, height);
        OnTabBoundsChanged(x, y, width, height);
    }

    /// <inheritdoc/>
    public virtual void OnTabBoundsChanged(double x, double y, double width, double height)
    {
    }

    /// <inheritdoc/>
    public void GetPointerPosition(out double x, out double y)
    {
        _trackingAdapter.GetPointerPosition(out x, out y);
    }

    /// <inheritdoc/>
    public void SetPointerPosition(double x, double y)
    {
        _trackingAdapter.SetPointerPosition(x, y);
        OnPointerPositionChanged(x, y);
    }

    /// <inheritdoc/>
    public virtual void OnPointerPositionChanged(double x, double y)
    {
    }

    /// <inheritdoc/>
    public void GetPointerScreenPosition(out double x, out double y)
    {
        _trackingAdapter.GetPointerScreenPosition(out x, out y);
    }

    /// <inheritdoc/>
    public void SetPointerScreenPosition(double x, double y)
    {
        _trackingAdapter.SetPointerScreenPosition(x, y);
        OnPointerScreenPositionChanged(x, y);
    }

    /// <inheritdoc/>
    public virtual void OnPointerScreenPositionChanged(double x, double y)
    {
    }
}