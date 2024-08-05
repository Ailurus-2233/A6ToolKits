using System;
using System.Collections;
using System.Collections.Generic;

namespace A6ToolKits.Common.Extensions;

public static class IListExtensions
{
    public static void ForEach<T>(this IList list, Action<T> action)
    {
        ArgumentNullException.ThrowIfNull(list);
        ArgumentNullException.ThrowIfNull(action);

        foreach (var item in list)
        {
            if (item is T arg)
            {
                action(arg);
            }
            else
            {
                throw new InvalidCastException($"Item in list cannot be cast to type {typeof(T)}");
            }
        }
    }
}