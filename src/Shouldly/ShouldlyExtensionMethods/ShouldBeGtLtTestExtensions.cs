namespace Shouldly;

public static partial class ShouldBeTestExtensions
{
    /// <summary>
    /// Asserts that the actual value is greater than the expected value using the specified comparer.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeGreaterThan<T>(this T? actual, T? expected, IComparer<T> comparer, string? customMessage = null)
    {
        actual.AssertAwesomely(_ => Is.GreaterThan(actual, expected, comparer), actual, expected, customMessage);
    }

    /// <summary>
    /// Asserts that the actual value is greater than the expected value.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeGreaterThan<T>(this T? actual, T? expected, string? customMessage = null)
        where T : IComparable<T>?
    {
        actual.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected, customMessage);
    }

    /// <summary>
    /// Asserts that the actual value is less than the expected value using the specified comparer.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeLessThan<T>(this T? actual, T? expected, IComparer<T> comparer, string? customMessage = null)
    {
        actual.AssertAwesomely(_ => Is.LessThan(actual, expected, comparer), actual, expected, customMessage);
    }

    /// <summary>
    /// Asserts that the actual value is less than the expected value.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeLessThan<T>(this T? actual, T? expected, string? customMessage = null)
        where T : IComparable<T>?
    {
        actual.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected, customMessage);
    }

    /// <summary>
    /// Asserts that the actual value is greater than or equal to the expected value using the specified comparer.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeGreaterThanOrEqualTo<T>(this T? actual, T? expected, IComparer<T> comparer, string? customMessage = null)
    {
        actual.AssertAwesomely(_ => Is.GreaterThanOrEqualTo(actual, expected, comparer), actual, expected, customMessage);
    }

    /// <summary>
    /// Asserts that the actual value is greater than or equal to the expected value.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeGreaterThanOrEqualTo<T>(this T? actual, T? expected, string? customMessage = null)
        where T : IComparable<T>?
    {
        actual.AssertAwesomely(v => Is.GreaterThanOrEqualTo(v, expected), actual, expected, customMessage);
    }

    /// <summary>
    /// Asserts that the actual value is less than or equal to the expected value using the specified comparer.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeLessThanOrEqualTo<T>(this T? actual, T? expected, IComparer<T> comparer, string? customMessage = null)
    {
        actual.AssertAwesomely(_ => Is.LessThanOrEqualTo(actual, expected, comparer), actual, expected, customMessage);
    }

    /// <summary>
    /// Asserts that the actual value is less than or equal to the expected value.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeLessThanOrEqualTo<T>(this T? actual, T? expected, string? customMessage = null)
        where T : IComparable<T>?
    {
        actual.AssertAwesomely(v => Is.LessThanOrEqualTo(v, expected), actual, expected, customMessage);
    }
}