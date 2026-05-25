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
    [MethodImpl(MethodImplOptions.NoInlining)]
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
    [MethodImpl(MethodImplOptions.NoInlining)]
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
    [MethodImpl(MethodImplOptions.NoInlining)]
    [ContractAnnotation("actual:null,expected:null => halt")]
    public static void ShouldNotBe<T>(this T? actual, T? expected, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual.AssertAwesomely(v => !Is.Equal(v, expected), actual, expected, customMessage, actualExpression: actualExpression);
    }

    /// <summary>
    /// Asserts that an actual value is not equal to the expected value using the specified comparer
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [ContractAnnotation("actual:null,expected:null => halt")]
    public static void ShouldNotBe<T>(this T? actual, T? expected, IEqualityComparer<T> comparer, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual.AssertAwesomely(v => !Is.Equal(v, expected, comparer), actual, expected, customMessage, actualExpression: actualExpression);
    }

    /// <summary>
    /// Asserts that an enumerable is equal to another enumerable, optionally ignoring order.
    /// Pass <paramref name="ignoreOrder"/> as a named argument (<c>ignoreOrder: true</c>).
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [OverloadResolutionPriority(1)]
    public static void ShouldBe<T>(
        [NotNullIfNotNull(nameof(expected))] this IEnumerable<T>? actual,
        [NotNullIfNotNull(nameof(actual))] IEnumerable<T>? expected,
        string? customMessage = null,
        bool ignoreOrder = false,
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
    /// Obsolete legacy overload kept so existing callers that pass <c>ignoreOrder</c>
    /// positionally (e.g. <c>actual.ShouldBe(expected, true)</c>) keep compiling. Migrate to
    /// the named form (<c>actual.ShouldBe(expected, ignoreOrder: true)</c>) or the
    /// <c>customMessage</c>-first overload.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [Obsolete("Use the customMessage-first overload, or pass ignoreOrder as a named argument: actual.ShouldBe(expected, ignoreOrder: true).")]
    public static void ShouldBe<T>(
        [NotNullIfNotNull(nameof(expected))] this IEnumerable<T>? actual,
        [NotNullIfNotNull(nameof(actual))] IEnumerable<T>? expected,
        bool ignoreOrder,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        ShouldBe(actual, expected, customMessage, ignoreOrder, actualExpression);

    /// <summary>
    /// Asserts that an enumerable is equal to another enumerable using the specified comparer,
    /// optionally ignoring order. Pass <paramref name="ignoreOrder"/> as a named argument
    /// (<c>ignoreOrder: true</c>).
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [OverloadResolutionPriority(1)]
    public static void ShouldBe<T>(
        [NotNullIfNotNull(nameof(expected))] this IEnumerable<T>? actual,
        [NotNullIfNotNull(nameof(actual))] IEnumerable<T>? expected,
        IEqualityComparer<T> comparer,
        string? customMessage = null,
        bool ignoreOrder = false,
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
    /// Obsolete legacy overload kept so existing callers that pass <c>ignoreOrder</c>
    /// positionally keep compiling. Migrate to the named form
    /// (<c>actual.ShouldBe(expected, comparer, ignoreOrder: true)</c>) or the
    /// <c>customMessage</c>-first overload.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [Obsolete("Use the customMessage-first overload, or pass ignoreOrder as a named argument: actual.ShouldBe(expected, comparer, ignoreOrder: true).")]
    public static void ShouldBe<T>(
        [NotNullIfNotNull(nameof(expected))] this IEnumerable<T>? actual,
        [NotNullIfNotNull(nameof(actual))] IEnumerable<T>? expected,
        IEqualityComparer<T> comparer,
        bool ignoreOrder,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null) =>
        ShouldBe(actual, expected, comparer, customMessage, ignoreOrder, actualExpression);

    /// <summary>
    /// Asserts that a decimal enumerable is equal to another decimal enumerable within the specified tolerance
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBe(this IEnumerable<decimal> actual, IEnumerable<decimal> expected, decimal tolerance, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected, tolerance, customMessage, actualExpression: actualExpression);
    }

    /// <summary>
    /// Asserts that an object is the same instance as another object
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
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
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotBeSameAs(this object? actual, object? expected, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        actual.AssertAwesomely(v => !Is.Same(v, expected), actual, expected, customMessage, actualExpression: actualExpression);
    }
}