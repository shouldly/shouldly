using Shouldly.Internals.Failures;

namespace Shouldly;

/// <summary>
/// Extension methods for comparison assertions
/// </summary>
public static partial class ShouldBeTestExtensions
{
    /// <summary>
    /// Asserts that the actual value is greater than the expected value using the specified comparer.
    /// </summary>
    public static void ShouldBeGreaterThan<T>(
        this T? actual,
        T? expected,
        IComparer<T> comparer,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (!actual.Satisfies(_ => Is.GreaterThan(actual, expected, comparer)))
            throw Fail.ShouldBeGreaterThan.With(expected).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the actual value is greater than the expected value.
    /// </summary>
    public static void ShouldBeGreaterThan<T>(
        this T? actual,
        T? expected,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where T : IComparable<T>?
    {
        if (!actual.Satisfies(v => Is.GreaterThan(v, expected)))
            throw Fail.ShouldBeGreaterThan.With(expected).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the actual value is less than the expected value using the specified comparer.
    /// </summary>
    public static void ShouldBeLessThan<T>(
        this T? actual,
        T? expected,
        IComparer<T> comparer,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (!actual.Satisfies(_ => Is.LessThan(actual, expected, comparer)))
            throw Fail.ShouldBeLessThan.With(expected).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the actual value is less than the expected value.
    /// </summary>
    public static void ShouldBeLessThan<T>(
        this T? actual,
        T? expected,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where T : IComparable<T>?
    {
        if (!actual.Satisfies(v => Is.LessThan(v, expected)))
            throw Fail.ShouldBeLessThan.With(expected).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the actual value is greater than or equal to the expected value using the specified comparer.
    /// </summary>
    public static void ShouldBeGreaterThanOrEqualTo<T>(
        this T? actual,
        T? expected,
        IComparer<T> comparer,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (!actual.Satisfies(_ => Is.GreaterThanOrEqualTo(actual, expected, comparer)))
            throw Fail.ShouldBeGreaterThanOrEqualTo.With(expected).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the actual value is greater than or equal to the expected value.
    /// </summary>
    public static void ShouldBeGreaterThanOrEqualTo<T>(
        this T? actual,
        T? expected,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where T : IComparable<T>?
    {
        if (!actual.Satisfies(v => Is.GreaterThanOrEqualTo(v, expected)))
            throw Fail.ShouldBeGreaterThanOrEqualTo.With(expected).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the actual value is less than or equal to the expected value using the specified comparer.
    /// </summary>
    public static void ShouldBeLessThanOrEqualTo<T>(
        this T? actual,
        T? expected,
        IComparer<T> comparer,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (!actual.Satisfies(_ => Is.LessThanOrEqualTo(actual, expected, comparer)))
            throw Fail.ShouldBeLessThanOrEqualTo.With(expected).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the actual value is less than or equal to the expected value.
    /// </summary>
    public static void ShouldBeLessThanOrEqualTo<T>(
        this T? actual,
        T? expected,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where T : IComparable<T>?
    {
        if (!actual.Satisfies(v => Is.LessThanOrEqualTo(v, expected)))
            throw Fail.ShouldBeLessThanOrEqualTo.With(expected).For(actual, customMessage, actualExpression);
    }
}
