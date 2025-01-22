using System.ComponentModel;
using JetBrains.Annotations;
using Shouldly.Internals;

namespace Shouldly;

[DebuggerStepThrough]
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldBeTestExtensions
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    [ContractAnnotation("actual:null,expected:notnull => halt;actual:notnull,expected:null => halt")]
    public static void ShouldBe<T>(
        [NotNullIfNotNull(nameof(expected))] this T? actual,
        [NotNullIfNotNull(nameof(actual))] T? expected,
        string? customMessage = null)
    {
        if (ShouldlyConfiguration.CompareAsObjectTypes.Contains(typeof(T).FullName!) || typeof(T) == typeof(string))
            actual.AssertAwesomely(v => Is.Equal(v, expected, new ObjectEqualityComparer<T>()), actual, expected, customMessage);
        else
            actual.AssertAwesomely(v => Is.Equal(v, expected), actual, expected, customMessage);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBe<T>(
        [NotNullIfNotNull(nameof(expected))] this T? actual,
        [NotNullIfNotNull(nameof(actual))] T? expected,
        IEqualityComparer<T> comparer,
        string? customMessage = null)
    {
        actual.AssertAwesomely(v => Is.Equal(v, expected, comparer), actual, expected, customMessage);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    [ContractAnnotation("actual:null,expected:null => halt")]
    public static void ShouldNotBe<T>(this T? actual, T? expected, string? customMessage = null)
    {
        actual.AssertAwesomely(v => !Is.Equal(v, expected), actual, expected, customMessage);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    [ContractAnnotation("actual:null,expected:null => halt")]
    public static void ShouldNotBe<T>(this T? actual, T? expected, IEqualityComparer<T> comparer, string? customMessage = null)
    {
        actual.AssertAwesomely(v => !Is.Equal(v, expected, comparer), actual, expected, customMessage);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBe<T>(
        [NotNullIfNotNull(nameof(expected))] this IEnumerable<T>? actual,
        [NotNullIfNotNull(nameof(actual))] IEnumerable<T>? expected,
        bool ignoreOrder = false)
    {
        ShouldBe(actual, expected, ignoreOrder, (string?)null);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBe<T>(
        [NotNullIfNotNull(nameof(expected))] this IEnumerable<T>? actual,
        [NotNullIfNotNull(nameof(actual))] IEnumerable<T>? expected,
        bool ignoreOrder,
        string? customMessage)
    {
        actual = EnumerableProxy<T>.WrapNonCollection(actual);
        expected = EnumerableProxy<T>.WrapNonCollection(expected);

        if (!ignoreOrder && ShouldlyConfiguration.CompareAsObjectTypes.Contains(typeof(T).FullName!))
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, new ObjectEqualityComparer<IEnumerable<T>?>()), actual, expected, customMessage);
        }
        else
        {
            if (ignoreOrder)
            {
                actual.AssertAwesomelyIgnoringOrder(v => Is.EqualIgnoreOrder(v, expected), actual, expected, customMessage);
            }
            else
            {
                actual.AssertAwesomely(v => Is.Equal(v, expected), actual, expected, customMessage);
            }
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBe<T>(
        [NotNullIfNotNull(nameof(expected))] this IEnumerable<T>? actual,
        [NotNullIfNotNull(nameof(actual))] IEnumerable<T>? expected,
        IEqualityComparer<T> comparer,
        bool ignoreOrder = false,
        string? customMessage = null)
    {
        if (ignoreOrder)
        {
            actual.AssertAwesomelyIgnoringOrder(v => Is.EqualIgnoreOrder(v, expected, comparer), actual, expected, customMessage);
        }
        else
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, comparer), actual, expected, customMessage);
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBe(this IEnumerable<decimal> actual, IEnumerable<decimal> expected, decimal tolerance, string? customMessage = null)
    {
        actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected, tolerance, customMessage);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeSameAs(
        [NotNullIfNotNull(nameof(expected))] this object? actual,
        [NotNullIfNotNull(nameof(actual))] object? expected,
        string? customMessage = null)
    {
        actual.AssertAwesomely(v => Is.Same(v, expected), actual, expected, customMessage);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotBeSameAs(this object? actual, object? expected, string? customMessage = null)
    {
        actual.AssertAwesomely(v => !Is.Same(v, expected), actual, expected, customMessage);
    }
}