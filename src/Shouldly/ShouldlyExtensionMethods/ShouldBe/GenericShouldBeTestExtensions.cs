using System.ComponentModel;
using JetBrains.Annotations;
using Shouldly.Internals;

namespace Shouldly;

[DebuggerStepThrough]
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldBeTestExtensions
{
    /// <summary>
    /// Asserts that an actual value is equal to the expected value
    /// </summary>
    [ContractAnnotation("actual:null,expected:notnull => halt;actual:notnull,expected:null => halt")]
    public static void ShouldBe<T>(
        [NotNullIfNotNull(nameof(expected))] this T? actual,
        [NotNullIfNotNull(nameof(actual))] T? expected,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (ShouldlyConfiguration.CompareAsObjectTypes.Contains(typeof(T).FullName!) || typeof(T) == typeof(string))
            actual.AssertAwesomely(v => Is.Equal(v, expected, new ObjectEqualityComparer<T>()), actual, expected, customMessage, actualExpression: actualExpression);
        else
            actual.AssertAwesomely(v => Is.Equal(v, expected), actual, expected, customMessage, actualExpression: actualExpression);
    }

    /// <summary>
    /// Asserts that an actual value is equal to the expected value using the specified comparer
    /// </summary>
    public static void ShouldBe<T>(
        [NotNullIfNotNull(nameof(expected))] this T? actual,
        [NotNullIfNotNull(nameof(actual))] T? expected,
        IEqualityComparer<T> comparer,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual.AssertAwesomely(v => Is.Equal(v, expected, comparer), actual, expected, customMessage, actualExpression: actualExpression);
    }

    /// <summary>
    /// Asserts that an actual value is not equal to the expected value
    /// </summary>
    [ContractAnnotation("actual:null,expected:null => halt")]
    public static void ShouldNotBe<T>(this T? actual, T? expected, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual.AssertAwesomely(v => !Is.Equal(v, expected), actual, expected, customMessage, actualExpression: actualExpression);
    }

    /// <summary>
    /// Asserts that an actual value is not equal to the expected value using the specified comparer
    /// </summary>
    [ContractAnnotation("actual:null,expected:null => halt")]
    public static void ShouldNotBe<T>(this T? actual, T? expected, IEqualityComparer<T> comparer, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual.AssertAwesomely(v => !Is.Equal(v, expected, comparer), actual, expected, customMessage, actualExpression: actualExpression);
    }

    /// <summary>
    /// Asserts that an enumerable is equal to another enumerable.
    /// </summary>
    // This overload deliberately has the same parameter count (arity) as the scalar
    // ShouldBe<T>(T?, T?, ...). When the arities match, the C# "more specific parameter type"
    // tie-break selects this enumerable overload over the scalar one at every LangVersion —
    // [OverloadResolutionPriority] is only honoured on C# 13+, so we must not rely on it alone
    // to disambiguate a core overload set (see issue #1278). The ignoreOrder flag lives on the
    // separate overload below so this arity stays aligned.
    [OverloadResolutionPriority(1)]
    public static void ShouldBe<T>(
        [NotNullIfNotNull(nameof(expected))] this IEnumerable<T>? actual,
        [NotNullIfNotNull(nameof(actual))] IEnumerable<T>? expected,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual.ShouldBe(expected, ignoreOrder: false, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that an enumerable is equal to another enumerable, optionally ignoring order.
    /// </summary>
    [OverloadResolutionPriority(1)]
    public static void ShouldBe<T>(
        [NotNullIfNotNull(nameof(expected))] this IEnumerable<T>? actual,
        [NotNullIfNotNull(nameof(actual))] IEnumerable<T>? expected,
        bool ignoreOrder,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual = EnumerableProxy<T>.WrapNonCollection(actual);
        expected = EnumerableProxy<T>.WrapNonCollection(expected);

        if (!ignoreOrder && ShouldlyConfiguration.CompareAsObjectTypes.Contains(typeof(T).FullName!))
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, new ObjectEqualityComparer<IEnumerable<T>?>()), actual, expected, customMessage, actualExpression: actualExpression);
        }
        else
        {
            if (ignoreOrder)
            {
                actual.AssertAwesomelyIgnoringOrder(v => Is.EqualIgnoreOrder(v, expected), actual, expected, customMessage, actualExpression: actualExpression);
            }
            else
            {
                actual.AssertAwesomely(v => Is.Equal(v, expected), actual, expected, customMessage, actualExpression: actualExpression);
            }
        }
    }

    /// <summary>
    /// Asserts that an enumerable is equal to another enumerable using the specified comparer, optionally ignoring order.
    /// </summary>
    public static void ShouldBe<T>(
        [NotNullIfNotNull(nameof(expected))] this IEnumerable<T>? actual,
        [NotNullIfNotNull(nameof(actual))] IEnumerable<T>? expected,
        IEqualityComparer<T> comparer,
        bool ignoreOrder = false,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (ignoreOrder)
        {
            actual.AssertAwesomelyIgnoringOrder(v => Is.EqualIgnoreOrder(v, expected, comparer), actual, expected, customMessage, actualExpression: actualExpression);
        }
        else
        {
            actual.AssertAwesomely(v => Is.Equal(v, expected, comparer), actual, expected, customMessage, actualExpression: actualExpression);
        }
    }

    /// <summary>
    /// Asserts that a decimal enumerable is equal to another decimal enumerable within the specified tolerance
    /// </summary>
    public static void ShouldBe(this IEnumerable<decimal> actual, IEnumerable<decimal> expected, decimal tolerance, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected, tolerance, customMessage, actualExpression: actualExpression);
    }

    /// <summary>
    /// Asserts that an object is the same instance as another object
    /// </summary>
    public static void ShouldBeSameAs(
        [NotNullIfNotNull(nameof(expected))] this object? actual,
        [NotNullIfNotNull(nameof(actual))] object? expected,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual.AssertAwesomely(v => Is.Same(v, expected), actual, expected, customMessage, actualExpression: actualExpression);
    }

    /// <summary>
    /// Asserts that an object is not the same instance as another object
    /// </summary>
    public static void ShouldNotBeSameAs(this object? actual, object? expected, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual.AssertAwesomely(v => !Is.Same(v, expected), actual, expected, customMessage, actualExpression: actualExpression);
    }
}