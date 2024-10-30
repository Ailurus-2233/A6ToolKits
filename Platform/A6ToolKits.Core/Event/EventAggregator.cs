using System;
using System.Collections.Generic;
using A6ToolKits.Common.Attributes.MVVM;

namespace A6ToolKits.Event;

/// <summary>
///     事件聚合器
/// </summary>
public class EventAggregator: IEventAggregator
{
    private readonly Dictionary<Type, List<object>> _subscribers = new Dictionary<Type, List<object>>();
    
    /// <summary>
    ///     订阅事件
    /// </summary>
    /// <param name="action">
    ///     事件触发后执行操作
    /// </param>
    /// <typeparam name="TEvent">
    ///     事件类型
    /// </typeparam>
    public void Subscribe<TEvent>(Action<TEvent> action) where TEvent : IEvent
    {
        if (!_subscribers.ContainsKey(typeof(TEvent)))
        {
            _subscribers[typeof(TEvent)] = [];
        }
        _subscribers[typeof(TEvent)].Add(action);
    }

    /// <summary>
    ///     取消订阅
    /// </summary>
    /// <param name="action">
    ///     事件执行后执行操作
    /// </param>
    /// <typeparam name="TEvent">
    ///     事件类型
    /// </typeparam>
    public void Unsubscribe<TEvent>(Action<TEvent> action) where TEvent : IEvent
    {
        if (_subscribers.ContainsKey(typeof(TEvent)))
        {
            _subscribers[typeof(TEvent)].Remove(action);
        }
    }

    /// <summary>
    ///     发布事件
    /// </summary>
    /// <param name="eventToPublish">
    ///     具体事件
    /// </param>
    /// <typeparam name="TEvent">
    ///     事件类型
    /// </typeparam>
    public void Publish<TEvent>(TEvent eventToPublish) where TEvent : IEvent
    {
        if (!_subscribers.ContainsKey(eventToPublish.GetType())) return;
        foreach (var subscriber in _subscribers[eventToPublish.GetType()])
        {
            ((Action<TEvent>)subscriber)(eventToPublish);
        }
    }
}