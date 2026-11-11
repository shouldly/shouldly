using System.Collections.Concurrent;

namespace Shouldly.Internals;

/// <summary>
/// Which type's <see cref="object.Equals(object)"/> a value uses, and so whether its type defines its own equality,
/// rather than inheriting reference equality from <see cref="object"/> or field-by-field equality from
/// <see cref="ValueType"/>. Such a type has said what equality means, so Shouldly asks it before <c>CompareTo</c>,
/// which only says how to sort (issue #1428).
/// </summary>
/// <remarks>
/// Found from a delegate bound to the value's <c>Equals</c>, which the runtime resolves to the override itself, not
/// from <c>Type.GetMethod</c>: under Native AOT a type's methods have no reflection metadata unless something roots
/// them, so <c>GetMethod</c> finds only <see cref="object.Equals(object)"/> and every type looks like it doesn't override it.
/// </remarks>
static class OverriddenEquals
{
    private static readonly ConcurrentDictionary<Type, Type> DeclaringTypes = new();

    /// <summary>Whether <paramref name="value"/>'s type, or a base type other than <see cref="object"/> or
    /// <see cref="ValueType"/>, overrides <see cref="object.Equals(object)"/>.</summary>
    public static bool IsDefinedBy(object value) =>
        DeclaringType(value) is var declaringType &&
        declaringType != typeof(object) &&
        declaringType != typeof(ValueType);

    /// <summary>The type that declares the <see cref="object.Equals(object)"/> that <paramref name="value"/> uses.</summary>
    public static Type DeclaringType(object value)
    {
        var type = value.GetType();
        if (DeclaringTypes.TryGetValue(type, out var declaringType))
            return declaringType;

        Func<object?, bool> equals = value.Equals;
        declaringType = equals.Method.DeclaringType ?? typeof(object);
        DeclaringTypes.TryAdd(type, declaringType);
        return declaringType;
    }
}
