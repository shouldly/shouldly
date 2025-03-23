﻿namespace Shouldly;

public static partial class ShouldBeTestExtensions
{
    /// <summary>
    /// Asserts that a DateTime is equal to another DateTime within the specified tolerance
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBe(this DateTime actual, DateTime expected, TimeSpan tolerance, string? customMessage = null)
    {
        actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected, tolerance, customMessage);
    }

    /// <summary>
    /// Asserts that a DateTimeOffset is equal to another DateTimeOffset within the specified tolerance
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBe(this DateTimeOffset actual, DateTimeOffset expected, TimeSpan tolerance, string? customMessage = null)
    {
        actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected, tolerance, customMessage);
    }

    /// <summary>
    /// Asserts that a TimeSpan is equal to another TimeSpan within the specified tolerance
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBe(this TimeSpan actual, TimeSpan expected, TimeSpan tolerance, string? customMessage = null)
    {
        actual.AssertAwesomely(v => Is.Equal(v, expected, tolerance), actual, expected, tolerance, customMessage);
    }

    /// <summary>
    /// Asserts that a DateTime is not equal to another DateTime within the specified tolerance
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotBe(this DateTime actual, DateTime expected, TimeSpan tolerance, string? customMessage = null)
    {
        actual.AssertAwesomely(v => !Is.Equal(v, expected, tolerance), actual, expected, tolerance, customMessage);
    }

    /// <summary>
    /// Asserts that a DateTimeOffset is not equal to another DateTimeOffset within the specified tolerance
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotBe(this DateTimeOffset actual, DateTimeOffset expected, TimeSpan tolerance, string? customMessage = null)
    {
        actual.AssertAwesomely(v => !Is.Equal(v, expected, tolerance), actual, expected, tolerance, customMessage);
    }

    /// <summary>
    /// Asserts that a TimeSpan is not equal to another TimeSpan within the specified tolerance
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotBe(this TimeSpan actual, TimeSpan expected, TimeSpan tolerance, string? customMessage = null)
    {
        actual.AssertAwesomely(v => !Is.Equal(v, expected, tolerance), actual, expected, tolerance, customMessage);
    }
}