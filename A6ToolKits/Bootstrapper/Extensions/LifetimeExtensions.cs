using System;
using System.Reflection;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace A6ToolKits.Bootstrapper.Extensions;

public static class LifetimeExtensions
{
    public static void SubscribeGlobalEvents(this ClassicDesktopStyleApplicationLifetime lifetime)
    {
        var subscribeGlobalEventsMethod = typeof(ClassicDesktopStyleApplicationLifetime).GetMethod(
            "SubscribeGlobalEvents",
            BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
        subscribeGlobalEventsMethod?.Invoke(lifetime, null);
    }

    public static ClassicDesktopStyleApplicationLifetime PrepareLifetime(
        AppBuilder builder,
        string[] args,
        Action<IClassicDesktopStyleApplicationLifetime>? lifetimeBuilder)
    {
        var extensionsType = typeof(ClassicDesktopStyleApplicationLifetimeExtensions);
        var prepareLifetimeMethod =
            extensionsType.GetMethod("PrepareLifetime", BindingFlags.NonPublic | BindingFlags.Static);

        if (prepareLifetimeMethod == null) throw new MissingMethodException("PrepareLifetime method not found.");

        return prepareLifetimeMethod.Invoke(null, [builder, args, lifetimeBuilder]) as
            ClassicDesktopStyleApplicationLifetime ?? throw new InvalidOperationException();
    }
}