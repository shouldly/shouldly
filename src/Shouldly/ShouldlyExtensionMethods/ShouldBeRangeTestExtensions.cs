namespace Shouldly;

public static partial class ShouldBeTestExtensions
{
    /// <summary>
    /// Asserts that the actual value is one of the expected values.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeOneOf<T>(this T? actual, params T[] expected)
    {
        ShouldBeOneOf(actual, expected, null);
    }

    /// <summary>
    /// Asserts that the actual value is one of the expected values.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeOneOf<T>(this T? actual, T[] expected, string? customMessage)
    {
        // Enumerable.Contains on an array always tolerates null.
        if (!expected.Contains(actual!))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual, customMessage).ToString());
    }

    /// <summary>
    /// Asserts that the actual value is one of the expected values using the specified comparer.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeOneOf<T>(this T? actual, T[] expected, IEqualityComparer<T> comparer, string? customMessage = null)
    {
        // Enumerable.Contains on an array always tolerates null.
        if (!expected.Contains(actual!, comparer))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual, customMessage).ToString());
    }

    /// <summary>
    /// Asserts that the actual value is not one of the expected values.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotBeOneOf<T>(this T? actual, params T[] expected)
    {
        ShouldNotBeOneOf(actual, expected, null);
    }

    /// <summary>
    /// Asserts that the actual value is not one of the expected values.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotBeOneOf<T>(this T? actual, T[] expected, string? customMessage)
    {
        // Enumerable.Contains on an array always tolerates null.
        if (expected.Contains(actual!))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual, customMessage).ToString());
    }

    /// <summary>
    /// Asserts that the actual value is not one of the expected values using the specified comparer.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotBeOneOf<T>(this T? actual, T[] expected, IEqualityComparer<T> comparer, string? customMessage = null)
    {
        // Enumerable.Contains on an array always tolerates null.
        if (expected.Contains(actual!, comparer))
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(expected, actual, customMessage).ToString());
    }

    /// <summary>
    /// Asserts that the actual value is within the specified range.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeInRange<T>([DisallowNull] this T actual, T? from, T? to, string? customMessage = null)
        where T : IComparable<T>
    {
        actual.AssertAwesomely(v => Is.InRange(v, from, to), actual, new { from, to }, customMessage);
    }

    /// <summary>
    /// Asserts that the actual value is not within the specified range.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotBeInRange<T>([DisallowNull] this T actual, T? from, T? to, string? customMessage = null)
        where T : IComparable<T>
    {
        actual.AssertAwesomely(v => !Is.InRange(v, from, to), actual, new { from, to }, customMessage);
    }
}