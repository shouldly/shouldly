using System.ComponentModel;
using JetBrains.Annotations;
using Shouldly.Internals;
using Shouldly.Internals.Failures;

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
        if (!actual.Satisfies(v => Equality.Equal(v, expected)))
            throw Fail.ShouldBe.With(expected).For(actual, customMessage, actualExpression);
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
        if (!actual.Satisfies(v => Equality.Equal(v, expected, comparer)))
            throw Fail.ShouldBe.With(expected, ComparisonRecord.ComparedValuesWithCustomComparer()).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that an actual value is not equal to the expected value
    /// </summary>
    [ContractAnnotation("actual:null,expected:null => halt")]
    public static void ShouldNotBe<T>(this T? actual, T? expected, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (!actual.Satisfies(v => !Equality.Equal(v, expected)))
            throw Fail.ShouldNotBe.With(expected).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that an actual value is not equal to the expected value using the specified comparer
    /// </summary>
    [ContractAnnotation("actual:null,expected:null => halt")]
    public static void ShouldNotBe<T>(this T? actual, T? expected, IEqualityComparer<T> comparer, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (!actual.Satisfies(v => !Equality.Equal(v, expected, comparer)))
            throw Fail.ShouldNotBe.With(expected, ComparisonRecord.ComparedValuesWithCustomComparer()).For(actual, customMessage, actualExpression);
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
        actual = NullEquivalence.Normalize(actual);
        expected = NullEquivalence.Normalize(expected);

        using var capturedActual = CapturedSequence.Of(actual);
        using var capturedExpected = CapturedSequence.Of(expected);
        actual = capturedActual.Items;
        expected = capturedExpected.Items;

        if (ignoreOrder)
        {
            if (!actual.Satisfies(v => Equality.SequenceEqual(v, expected, ignoreOrder: true)))
                throw Fail.ShouldBe.IgnoringOrder(expected, ComparisonRecord.ComparedElementsWith(Equality.ComparerFor<T>(), isCustom: false)).For(actual, customMessage, actualExpression);
        }
        else
        {
            if (!actual.Satisfies(v => Equality.SequenceEqual(v, expected)))
                throw Fail.ShouldBe.With(expected).For(actual, customMessage, actualExpression);
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
        actual = NullEquivalence.Normalize(actual);
        expected = NullEquivalence.Normalize(expected);

        using var capturedActual = CapturedSequence.Of(actual);
        using var capturedExpected = CapturedSequence.Of(expected);
        actual = capturedActual.Items;
        expected = capturedExpected.Items;

        if (ignoreOrder)
        {
            if (!actual.Satisfies(v => Equality.SequenceEqual(v, expected, ignoreOrder: true, comparer)))
                throw Fail.ShouldBe.IgnoringOrder(expected, ComparisonRecord.ComparedElementsWith(comparer, isCustom: true)).For(actual, customMessage, actualExpression);
        }
        else
        {
            if (!actual.Satisfies(v => Equality.SequenceEqual(v, expected, comparer: comparer)))
                throw Fail.ShouldBe.With(expected, ComparisonRecord.ComparedElementsWith(comparer, isCustom: true)).For(actual, customMessage, actualExpression);
        }
    }

    /// <summary>
    /// Asserts that a decimal enumerable is equal to another decimal enumerable within the specified tolerance
    /// </summary>
    public static void ShouldBe(this IEnumerable<decimal> actual, IEnumerable<decimal> expected, decimal tolerance, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        using var capturedActual = CapturedSequence.Of(actual);
        using var capturedExpected = CapturedSequence.Of(expected);
        actual = capturedActual.Items!;
        expected = capturedExpected.Items!;
        var comparer = Equality.WithinTolerance<decimal>(tolerance);
        if (!actual.Satisfies(v => Equality.SequenceEqual(v, expected, comparer: comparer)))
            throw Fail.ShouldBe.Within(expected, tolerance, comparer).For(actual, customMessage, actualExpression);
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
        if (!actual.Satisfies(v => Is.Same(v, expected)))
            throw Fail.ShouldBeSameAs.With(expected).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that an object is not the same instance as another object
    /// </summary>
    public static void ShouldNotBeSameAs(this object? actual, object? expected, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (!actual.Satisfies(v => !Is.Same(v, expected)))
            throw Fail.ShouldNotBeSameAs.With(expected).For(actual, customMessage, actualExpression);
    }
}