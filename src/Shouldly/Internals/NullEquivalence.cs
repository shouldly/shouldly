#if !NETSTANDARD2_0
using System.Collections.Immutable;
#endif

namespace Shouldly.Internals;

/// <summary>
/// The values Shouldly treats as null: <c>null</c> itself and a default <c>ImmutableArray&lt;T&gt;</c>. A default
/// <c>ImmutableArray&lt;T&gt;</c> is a struct wrapping a null array. Boxed, it isn't null, but enumerating it throws
/// <see cref="InvalidOperationException"/>, so code that checks for null and then enumerates fails on it. Comparing,
/// formatting and walking values through this class makes it behave like a null collection instead (issue #1406).
/// </summary>
static class NullEquivalence
{
#if !NETSTANDARD2_0
    // ImmutableArray<T>.Equals(object) compares the wrapped arrays of any two ImmutableArrays, whatever their T, so
    // a value equals this exactly when it wraps a null array. That avoids reflection and MakeGenericType, keeping
    // the check trim- and AOT-safe.
    private static readonly object DefaultImmutableArray = default(ImmutableArray<object>);
#endif

    /// <summary>
    /// Whether <paramref name="value"/> is <c>null</c> or a default <c>ImmutableArray&lt;T&gt;</c>.
    /// </summary>
    public static bool IsNull([NotNullWhen(false)] object? value) =>
        value is null || IsDefaultImmutableArray(value);

    /// <summary>
    /// <c>null</c> when <paramref name="value"/> is a default <c>ImmutableArray&lt;T&gt;</c>; otherwise
    /// <paramref name="value"/>.
    /// </summary>
    public static T? Normalize<T>(T? value)
        where T : class =>
        IsDefaultImmutableArray(value) ? null : value;

    private static bool IsDefaultImmutableArray(object? value)
    {
        if (value is not ValueType)
            return false;

        var type = value.GetType();
        if (!type.IsGenericType)
            return false;

#if NETSTANDARD2_0
        // System.Collections.Immutable isn't referenced on this target, so recognise the type by name and read its
        // IsDefault property. The AOT analyzers don't run on netstandard2.0, and AOT apps use the net8.0+ assets.
        var definition = type.GetGenericTypeDefinition();
        return definition.FullName == "System.Collections.Immutable.ImmutableArray`1" &&
               definition.Assembly.GetName().Name == "System.Collections.Immutable" &&
               type.GetProperty("IsDefault")?.GetValue(value) is true;
#else
        return type.GetGenericTypeDefinition() == typeof(ImmutableArray<>) && value.Equals(DefaultImmutableArray);
#endif
    }
}
