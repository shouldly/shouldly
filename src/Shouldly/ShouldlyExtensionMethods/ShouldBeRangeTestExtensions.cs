using Shouldly.Internals;
using Shouldly.Internals.Failures;

namespace Shouldly;

public static partial class ShouldBeTestExtensions
{
    /// <summary>
    /// Asserts that the actual value is one of the expected values, comparing as <c>ShouldBe</c> does.
    /// </summary>
    public static void ShouldBeOneOf<T>(this T? actual, T[] expected, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (!Membership.IsOneOf(actual, expected))
            throw Fail.ShouldBeOneOf.With(expected).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the actual value is one of the expected values using the specified comparer.
    /// </summary>
    public static void ShouldBeOneOf<T>(this T? actual, T[] expected, IEqualityComparer<T> comparer, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (!Membership.IsOneOf(actual, expected, comparer))
            throw Fail.ShouldBeOneOf.With(expected).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the actual value is not one of the expected values, comparing as <c>ShouldBe</c> does.
    /// </summary>
    public static void ShouldNotBeOneOf<T>(this T? actual, T[] expected, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (Membership.IsOneOf(actual, expected))
            throw Fail.ShouldNotBeOneOf.With(expected).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the actual value is not one of the expected values using the specified comparer.
    /// </summary>
    public static void ShouldNotBeOneOf<T>(this T? actual, T[] expected, IEqualityComparer<T> comparer, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (Membership.IsOneOf(actual, expected, comparer))
            throw Fail.ShouldNotBeOneOf.With(expected).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the actual value is within the specified range.
    /// </summary>
    public static void ShouldBeInRange<T>([DisallowNull] this T actual, T? from, T? to, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where T : IComparable<T>
    {
        if (!actual.Satisfies(v => Is.InRange(v, from, to)))
            throw Fail.ShouldBeInRange.With(new { from, to }).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the actual value is not within the specified range.
    /// </summary>
    public static void ShouldNotBeInRange<T>([DisallowNull] this T actual, T? from, T? to, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
        where T : IComparable<T>
    {
        if (!actual.Satisfies(v => !Is.InRange(v, from, to)))
            throw Fail.ShouldNotBeInRange.With(new { from, to }).For(actual, customMessage, actualExpression);
    }
}