using Shouldly.Internals.Failures;

namespace Shouldly;

public static partial class ShouldBeTestExtensions
{
    /// <summary>
    /// Asserts that the decimal value is positive.
    /// </summary>
    public static void ShouldBePositive(this decimal actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var expected = default(decimal);
        if (!actual.Satisfies(v => Is.GreaterThan(v, expected)))
            throw Fail.ShouldBePositive.For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the decimal value is negative.
    /// </summary>
    public static void ShouldBeNegative(this decimal actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var expected = default(decimal);
        if (!actual.Satisfies(v => Is.LessThan(v, expected)))
            throw Fail.ShouldBeNegative.For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the double value is positive.
    /// </summary>
    public static void ShouldBePositive(this double actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var expected = default(double);
        if (!actual.Satisfies(v => Is.GreaterThan(v, expected)))
            throw Fail.ShouldBePositive.For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the double value is negative.
    /// </summary>
    public static void ShouldBeNegative(this double actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var expected = default(double);
        if (!actual.Satisfies(v => Is.LessThan(v, expected)))
            throw Fail.ShouldBeNegative.For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the float value is positive.
    /// </summary>
    public static void ShouldBePositive(this float actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var expected = default(float);
        if (!actual.Satisfies(v => Is.GreaterThan(v, expected)))
            throw Fail.ShouldBePositive.For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the float value is negative.
    /// </summary>
    public static void ShouldBeNegative(this float actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var expected = default(float);
        if (!actual.Satisfies(v => Is.LessThan(v, expected)))
            throw Fail.ShouldBeNegative.For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the integer value is positive.
    /// </summary>
    public static void ShouldBePositive(this int actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var expected = default(int);
        if (!actual.Satisfies(v => Is.GreaterThan(v, expected)))
            throw Fail.ShouldBePositive.For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the integer value is negative.
    /// </summary>
    public static void ShouldBeNegative(this int actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var expected = default(int);
        if (!actual.Satisfies(v => Is.LessThan(v, expected)))
            throw Fail.ShouldBeNegative.For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the long value is positive.
    /// </summary>
    public static void ShouldBePositive(this long actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var expected = default(long);
        if (!actual.Satisfies(v => Is.GreaterThan(v, expected)))
            throw Fail.ShouldBePositive.For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the long value is negative.
    /// </summary>
    public static void ShouldBeNegative(this long actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var expected = default(long);
        if (!actual.Satisfies(v => Is.LessThan(v, expected)))
            throw Fail.ShouldBeNegative.For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the short value is positive.
    /// </summary>
    public static void ShouldBePositive(this short actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var expected = default(short);
        if (!actual.Satisfies(v => Is.GreaterThan(v, expected)))
            throw Fail.ShouldBePositive.For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the short value is negative.
    /// </summary>
    public static void ShouldBeNegative(this short actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var expected = default(short);
        if (!actual.Satisfies(v => Is.LessThan(v, expected)))
            throw Fail.ShouldBeNegative.For(actual, customMessage, actualExpression);
    }
}