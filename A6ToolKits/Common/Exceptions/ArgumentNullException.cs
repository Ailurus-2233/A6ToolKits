using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace A6ToolKits.Common.Exceptions;

public static class ArgumentNullException
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfNull(
        [NotNull] object? argument,
        [CallerArgumentExpression("argument")] string? paramName = null)
    {
        if (argument != null)
            return;
        Throw(paramName);
    }


    [DoesNotReturn]
    private static void Throw(string? paramName) => throw new global::System.ArgumentNullException(paramName);


    public static class For<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfNull(
            [NotNull] T? argument,
            [CallerArgumentExpression("argument")] string? paramName = null)
        {
            if (argument != null)
                return;
            Throw(paramName);
        }
    }
}