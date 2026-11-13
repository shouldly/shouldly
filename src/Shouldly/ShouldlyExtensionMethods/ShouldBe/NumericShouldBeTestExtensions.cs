using Shouldly.Internals;
using Shouldly.Internals.Failures;

namespace Shouldly;

public static partial class ShouldBeTestExtensions
{
    /// <summary>
    /// Asserts that a float is equal to another float within the specified tolerance
    /// </summary>
    public static void ShouldBe(this float actual, float expected, double tolerance, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var equality = Equality.WithinTolerance<float>(tolerance);
        if (!actual.Satisfies(v => equality.Equal(v, expected)))
            throw Fail.ShouldBe.With(expected, equality).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that a collection of doubles is equal to another collection of doubles within the specified tolerance
    /// </summary>
    public static void ShouldBe(this IEnumerable<double> actual, IEnumerable<double> expected, double tolerance, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        using var capturedActual = CapturedSequence.Of(actual);
        using var capturedExpected = CapturedSequence.Of(expected);
        actual = capturedActual.Items!;
        expected = capturedExpected.Items!;
        var equality = Equality.OfSequences(Equality.WithinTolerance<double>(tolerance));
        if (!actual.Satisfies(v => equality.Equal(v, expected)))
            throw Fail.ShouldBe.With(expected, equality).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that a collection of floats is equal to another collection of floats within the specified tolerance
    /// </summary>
    public static void ShouldBe(this IEnumerable<float> actual, IEnumerable<float> expected, double tolerance, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        using var capturedActual = CapturedSequence.Of(actual);
        using var capturedExpected = CapturedSequence.Of(expected);
        actual = capturedActual.Items!;
        expected = capturedExpected.Items!;
        var equality = Equality.OfSequences(Equality.WithinTolerance<float>(tolerance));
        if (!actual.Satisfies(v => equality.Equal(v, expected)))
            throw Fail.ShouldBe.With(expected, equality).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that a double is equal to another double within the specified tolerance
    /// </summary>
    public static void ShouldBe(this double actual, double expected, double tolerance, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var equality = Equality.WithinTolerance<double>(tolerance);
        if (!actual.Satisfies(v => equality.Equal(v, expected)))
            throw Fail.ShouldBe.With(expected, equality).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that a decimal is equal to another decimal within the specified tolerance
    /// </summary>
    public static void ShouldBe(this decimal actual, decimal expected, decimal tolerance, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var equality = Equality.WithinTolerance<decimal>(tolerance);
        if (!actual.Satisfies(v => equality.Equal(v, expected)))
            throw Fail.ShouldBe.With(expected, equality).For(actual, customMessage, actualExpression);
    }
}