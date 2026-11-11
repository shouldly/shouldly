using System.Collections.Concurrent;
using System.Runtime.ExceptionServices;
using NUnit.Framework.Constraints;

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
    private static readonly ConcurrentDictionary<(Type Receiver, Type Interface, Type Declared), MethodInfo?> DeclaredTypeMethods = new();

    /// <summary>
    /// When <paramref name="expected"/> and <paramref name="actual"/> are both <see cref="KeyValuePair{TKey, TValue}"/>s,
    /// sets <paramref name="result"/> to whether their keys and their values are equal and returns <c>true</c>.
    /// Each component is compared as <see cref="Equality.ComparerFor{T}"/> compares a value of the expected pair's
    /// <c>TKey</c> or <c>TValue</c>: by a registered equality rule when one applies, and otherwise with
    /// <paramref name="elementComparer"/> plus the <c>IEquatable&lt;S&gt;</c>/<c>IComparable&lt;S&gt;</c> steps for the
    /// declared type <c>S</c> that it can't take itself. <c>Equals</c> is called on the expected component.
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

    /// <summary>
    /// When <paramref name="expected"/> and <paramref name="actual"/> are both <see cref="KeyValuePair{TKey, TValue}"/>s,
    /// names the first component <see cref="TryEquals"/> found unequal, <c>Key</c> or <c>Value</c>, and gives the two
    /// components, for a failure message that says where two pairs differ.
    /// </summary>
    public static bool TryFindUnequalComponent(object expected, object actual, IEqualityComparer elementComparer,
        [NotNullWhen(true)] out string? name, out object? expectedComponent, out object? actualComponent)
    {
        name = null;
        expectedComponent = actualComponent = null;
        if (Find(expected) is not { } expectedAccessors || Find(actual) is not { } actualAccessors)
            return false;

        foreach (var (expectedProperty, actualProperty) in new[]
                 {
                     (expectedAccessors.Key, actualAccessors.Key),
                     (expectedAccessors.Value, actualAccessors.Value)
                 })
        {
            if (ComponentEquals(expectedProperty, actualProperty, expected, actual, elementComparer))
                continue;

            name = expectedProperty.Name;
            expectedComponent = expectedProperty.GetValue(expected);
            actualComponent = actualProperty.GetValue(actual);
            return true;
        }

        return false;
    }

    private static bool ComponentEquals(PropertyInfo expectedProperty, PropertyInfo actualProperty, object expected, object actual, IEqualityComparer elementComparer)
    {
        var expectedComponent = expectedProperty.GetValue(expected);
        var actualComponent = actualProperty.GetValue(actual);

        // A rule decides before the declared-type steps below, as it does in EqualityComparer<S>, and likewise never
        // sees a null-equivalent value such as a default ImmutableArray<T>.
        if (!NullEquivalence.IsNull(expectedComponent) && !NullEquivalence.IsNull(actualComponent) &&
            EqualityRules.TryEquals(expectedComponent, actualComponent, out var ruleResult))
            return ruleResult;

        if (TryDeclaredTypeEquals(expectedProperty.PropertyType, expectedComponent, actualComponent, out var result))
            return result;

        return elementComparer.Equals(expectedComponent, actualComponent);
    }

    /// <summary>
    /// The <c>IEquatable&lt;S&gt;</c> and <c>IComparable&lt;S&gt;</c> steps that <c>EqualityComparer&lt;S&gt;</c> takes for
    /// the component's declared type <c>S</c>, which the object element comparer can't: it only knows <c>object</c>.
    /// Taken where <c>EqualityComparer&lt;S&gt;</c> takes them, after the null, numeric and enumerable cases, so the
    /// element comparer then finishes the cascade (runtime <c>IEquatable&lt;U&gt;</c>, an overridden
    /// <see cref="object.Equals(object)"/>, <see cref="IComparable"/>) as <c>EqualityComparer&lt;S&gt;</c> would.
    /// </summary>
    private static bool TryDeclaredTypeEquals(Type declaredType, object? expected, object? actual, out bool result)
    {
        result = false;
        if (expected is null || actual is null)
            return false;

        if (declaredType == typeof(object) ||
            ReferenceEquals(expected, actual) ||
            !declaredType.IsInstanceOfType(actual) ||
            (Numerics.IsNumericType(expected) && Numerics.IsNumericType(actual)) ||
            (expected.TryGetEnumerable(out _) && actual.TryGetEnumerable(out _)))
            return false;

        var receiverType = expected.GetType();
        if (FindDeclaredTypeMethod(receiverType, typeof(IEquatable<>), declaredType) is { } equals)
        {
            result = (bool)Invoke(equals, expected, actual);
            return true;
        }

        // A type that defines its own Equals is left to the element comparer, which asks it before CompareTo (#1428).
        if (!OverriddenEquals.IsDefinedBy(receiverType) &&
            FindDeclaredTypeMethod(receiverType, typeof(IComparable<>), declaredType) is { } compareTo)
        {
            result = (int)Invoke(compareTo, expected, actual) == 0;
            return true;
        }

        return false;
    }

    private static object Invoke(MethodInfo method, object receiver, object argument)
    {
        try
        {
            return method.Invoke(receiver, [argument])!;
        }
        catch (TargetInvocationException e) when (e.InnerException is not null)
        {
            // Surface the user's own exception, as a direct call would.
            ExceptionDispatchInfo.Capture(e.InnerException).Throw();
            throw;
        }
    }

    /// <summary>
    /// The method of the <paramref name="openInterface"/> (<c>IEquatable&lt;&gt;</c> or <c>IComparable&lt;&gt;</c>) that
    /// <c>x is IEquatable&lt;S&gt;</c> / <c>x is IComparable&lt;S&gt;</c> would bind to for a receiver of
    /// <paramref name="receiverType"/>: the interface closed over <c>S</c> itself or, both being contravariant, over a
    /// type <c>S</c> converts to by reference, preferring the most derived. Found on the receiver's interfaces, so no
    /// <c>MakeGenericType</c> is needed.
    /// </summary>
    private static MethodInfo? FindDeclaredTypeMethod(Type receiverType, Type openInterface, Type declaredType) =>
        DeclaredTypeMethods.GetOrAdd((receiverType, openInterface, declaredType),
            static key => FindDeclaredTypeMethodUncached(key.Receiver, key.Interface, key.Declared));

    // Without these, NativeAOT drops the interfaces' method metadata and GetMethod finds nothing.
#if NET5_0_OR_GREATER
    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(IEquatable<>))]
    [DynamicDependency(DynamicallyAccessedMemberTypes.PublicMethods, typeof(IComparable<>))]
#endif
    [UnconditionalSuppressMessage("Trimming", "IL2070",
        Justification = "Reads the interfaces of a type that has a live instance. If trimming removed an implementation, none is found and the comparison falls back to the element comparer.")]
    [UnconditionalSuppressMessage("Trimming", "IL2075",
        Justification = "IEquatable<T>.Equals and IComparable<T>.CompareTo are rooted by the DynamicDependency attributes above.")]
    private static MethodInfo? FindDeclaredTypeMethodUncached(Type receiverType, Type openInterface, Type declaredType)
    {
        Type? best = null;
        MethodInfo? bestMethod = null;
        foreach (var @interface in receiverType.GetInterfaces())
        {
            if (!@interface.IsGenericType || @interface.GetGenericTypeDefinition() != openInterface)
                continue;

            var argument = @interface.GenericTypeArguments[0];
            var matches = argument == declaredType ||
                          (!declaredType.IsValueType && !argument.IsValueType && argument.IsAssignableFrom(declaredType));
            if (matches && (best is null || best.IsAssignableFrom(argument)) &&
                @interface.GetMethod(openInterface == typeof(IEquatable<>) ? nameof(IEquatable<object>.Equals) : nameof(IComparable<object>.CompareTo)) is { } method)
            {
                best = argument;
                bestMethod = method;
            }
        }

        return bestMethod;
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
