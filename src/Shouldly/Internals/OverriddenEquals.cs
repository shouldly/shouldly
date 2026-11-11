using System.Collections.Concurrent;

namespace Shouldly.Internals;

/// <summary>
/// Whether a type defines its own <see cref="object.Equals(object)"/>, rather than inheriting reference equality from
/// <see cref="object"/> or field-by-field equality from <see cref="ValueType"/>. Such a type has said what equality
/// means, so Shouldly asks it before <c>CompareTo</c>, which only says how to sort (issue #1428).
/// </summary>
static class OverriddenEquals
{
    private static readonly ConcurrentDictionary<Type, bool> Cache = new();

    public static bool IsDefinedBy(Type type) =>
        Cache.GetOrAdd(type, static t => EqualsDeclaringType(t) is { } declaringType &&
                                         declaringType != typeof(object) &&
                                         declaringType != typeof(ValueType));

    [UnconditionalSuppressMessage("Trimming", "IL2070",
        Justification = "Equals(object) is virtual on System.Object, so it or its override is kept for any type that has a live instance.")]
    private static Type? EqualsDeclaringType(Type type) =>
        type.GetMethod(nameof(Equals), BindingFlags.Public | BindingFlags.Instance, binder: null, [typeof(object)], modifiers: null)?.DeclaringType;
}
