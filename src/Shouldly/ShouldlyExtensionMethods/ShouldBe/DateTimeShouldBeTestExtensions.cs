using Shouldly.Internals.Failures;

namespace Shouldly;

public static partial class ShouldBeTestExtensions
{
    /// <summary>
    /// Asserts that a DateTime is equal to another DateTime within the specified tolerance
    /// </summary>
    public static void ShouldBe(this DateTime actual, DateTime expected, TimeSpan tolerance, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var equality = Internals.Equality.WithinTolerance<DateTime>(tolerance);
        if (!actual.Satisfies(v => equality.Equal(v, expected)))
            throw Fail.ShouldBe.With(expected, equality).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that a DateTimeOffset is equal to another DateTimeOffset within the specified tolerance
    /// </summary>
    public static void ShouldBe(this DateTimeOffset actual, DateTimeOffset expected, TimeSpan tolerance, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var equality = Internals.Equality.WithinTolerance<DateTimeOffset>(tolerance);
        if (!actual.Satisfies(v => equality.Equal(v, expected)))
            throw Fail.ShouldBe.With(expected, equality).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that a TimeSpan is equal to another TimeSpan within the specified tolerance
    /// </summary>
    public static void ShouldBe(this TimeSpan actual, TimeSpan expected, TimeSpan tolerance, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var equality = Internals.Equality.WithinTolerance<TimeSpan>(tolerance);
        if (!actual.Satisfies(v => equality.Equal(v, expected)))
            throw Fail.ShouldBe.With(expected, equality).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that a DateTime is not equal to another DateTime within the specified tolerance
    /// </summary>
    public static void ShouldNotBe(this DateTime actual, DateTime expected, TimeSpan tolerance, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var equality = Internals.Equality.WithinTolerance<DateTime>(tolerance);
        if (!actual.Satisfies(v => !equality.Equal(v, expected)))
            throw Fail.ShouldNotBe.With(expected, equality).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that a DateTimeOffset is not equal to another DateTimeOffset within the specified tolerance
    /// </summary>
    public static void ShouldNotBe(this DateTimeOffset actual, DateTimeOffset expected, TimeSpan tolerance, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var equality = Internals.Equality.WithinTolerance<DateTimeOffset>(tolerance);
        if (!actual.Satisfies(v => !equality.Equal(v, expected)))
            throw Fail.ShouldNotBe.With(expected, equality).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that a TimeSpan is not equal to another TimeSpan within the specified tolerance
    /// </summary>
    public static void ShouldNotBe(this TimeSpan actual, TimeSpan expected, TimeSpan tolerance, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var equality = Internals.Equality.WithinTolerance<TimeSpan>(tolerance);
        if (!actual.Satisfies(v => !equality.Equal(v, expected)))
            throw Fail.ShouldNotBe.With(expected, equality).For(actual, customMessage, actualExpression);
    }
}