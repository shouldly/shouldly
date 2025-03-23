namespace Shouldly;

public static partial class ShouldBeTestExtensions
{
    /// <summary>
    /// Asserts that the decimal value is positive.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBePositive(this decimal actual, string? customMessage = null)
    {
        var expected = default(decimal);
        actual.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected, customMessage);
    }

    /// <summary>
    /// Asserts that the decimal value is negative.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeNegative(this decimal actual, string? customMessage = null)
    {
        var expected = default(decimal);
        actual.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected, customMessage);
    }

    /// <summary>
    /// Asserts that the double value is positive.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBePositive(this double actual, string? customMessage = null)
    {
        var expected = default(double);
        actual.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected, customMessage);
    }

    /// <summary>
    /// Asserts that the double value is negative.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeNegative(this double actual, string? customMessage = null)
    {
        var expected = default(double);
        actual.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected, customMessage);
    }

    /// <summary>
    /// Asserts that the float value is positive.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBePositive(this float actual, string? customMessage = null)
    {
        var expected = default(float);
        actual.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected, customMessage);
    }

    /// <summary>
    /// Asserts that the float value is negative.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeNegative(this float actual, string? customMessage = null)
    {
        var expected = default(float);
        actual.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected, customMessage);
    }

    /// <summary>
    /// Asserts that the integer value is positive.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBePositive(this int actual, string? customMessage = null)
    {
        var expected = default(int);
        actual.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected, customMessage);
    }

    /// <summary>
    /// Asserts that the integer value is negative.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeNegative(this int actual, string? customMessage = null)
    {
        var expected = default(int);
        actual.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected, customMessage);
    }

    /// <summary>
    /// Asserts that the long value is positive.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBePositive(this long actual, string? customMessage = null)
    {
        var expected = default(long);
        actual.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected, customMessage);
    }

    /// <summary>
    /// Asserts that the long value is negative.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeNegative(this long actual, string? customMessage = null)
    {
        var expected = default(long);
        actual.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected, customMessage);
    }

    /// <summary>
    /// Asserts that the short value is positive.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBePositive(this short actual, string? customMessage = null)
    {
        var expected = default(short);
        actual.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected, customMessage);
    }

    /// <summary>
    /// Asserts that the short value is negative.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeNegative(this short actual, string? customMessage = null)
    {
        var expected = default(short);
        actual.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected, customMessage);
    }
}