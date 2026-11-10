using System.Collections.Concurrent;

namespace Shouldly.Internals;

/// <summary>
/// Compares two boxed <see cref="KeyValuePair{TKey, TValue}"/>s of any type arguments as their key and their value,
/// so a dictionary's entries compare the way its values do under <c>ShouldBe</c> and <c>ShouldContainKeyAndValue</c>
/// rather than with <see cref="ValueType.Equals(object)"/>, which compares a collection-typed value by reference
/// (issue #1408).
/// </summary>
static class KeyValuePairs
{
    private static readonly ConcurrentDictionary<Type, Accessors?> AccessorsByType = new();

    /// <summary>
    /// When <paramref name="expected"/> and <paramref name="actual"/> are both <see cref="KeyValuePair{TKey, TValue}"/>s,
    /// sets <paramref name="result"/> to whether their keys and their values are equal and returns <c>true</c>.
    /// Each component is compared as <see cref="Is.ScalarComparerFor{T}"/> compares a value of the expected pair's
    /// <c>TKey</c> or <c>TValue</c>: with its own <see cref="object.Equals(object)"/> for <see cref="string"/> and the
    /// types in <see cref="ShouldlyConfiguration.CompareAsObjectTypes"/>, and otherwise with
    /// <paramref name="elementComparer"/>. <c>Equals</c> is called on the expected component.
    /// </summary>
    public static bool TryEquals(object expected, object actual, IEqualityComparer elementComparer, out bool result)
    {
        if (Find(expected) is not { } expectedAccessors || Find(actual) is not { } actualAccessors)
        {
            result = false;
            return false;
        }

        result =
            ComponentEquals(expectedAccessors.Key, actualAccessors.Key, expected, actual, elementComparer) &&
            ComponentEquals(expectedAccessors.Value, actualAccessors.Value, expected, actual, elementComparer);
        return true;
    }

    private static bool ComponentEquals(PropertyInfo expectedProperty, PropertyInfo actualProperty, object expected, object actual, IEqualityComparer elementComparer)
    {
        var expectedComponent = expectedProperty.GetValue(expected);
        var actualComponent = actualProperty.GetValue(actual);

        // Mirrors the condition in Is.ScalarComparerFor, which can't be called here without the static type.
        var componentType = expectedProperty.PropertyType;
        return componentType == typeof(string) || ShouldlyConfiguration.CompareAsObjectTypes.Contains(componentType.FullName!)
            ? Equals(expectedComponent, actualComponent)
            : elementComparer.Equals(expectedComponent, actualComponent);
    }

    private static Accessors? Find(object value) =>
        value is ValueType ? AccessorsByType.GetOrAdd(value.GetType(), static type => FindAccessors(type)) : null;

    // The getters are read from the already-constructed pair type, so no MakeGenericType is needed. Rooting the
    // generic definition's properties keeps them for every instantiation, as StringHelpers' pair formatting needs too.
#if NET5_0_OR_GREATER
    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicProperties, typeof(KeyValuePair<,>))]
#endif
    [UnconditionalSuppressMessage("Trimming", "IL2070",
        Justification = "KeyValuePair<TKey, TValue>'s public properties are rooted by the DynamicDependency above.")]
    private static Accessors? FindAccessors(Type type)
    {
        if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(KeyValuePair<,>))
            return null;

        return new(
            type.GetProperty(nameof(KeyValuePair<int, int>.Key))!,
            type.GetProperty(nameof(KeyValuePair<int, int>.Value))!);
    }

    private sealed record Accessors(PropertyInfo Key, PropertyInfo Value);
}
