﻿namespace Shouldly;

public static partial class ShouldBeTestExtensions
{
    /// <summary>
    /// decimal
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBePositive(this decimal actual, string? customMessage = null)
    {
        var expected = default(decimal);
        actual.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected, customMessage);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeNegative(this decimal actual, string? customMessage = null)
    {
        var expected = default(decimal);
        actual.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected, customMessage);
    }

    /// <summary>
    /// double
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBePositive(this double actual, string? customMessage = null)
    {
        var expected = default(double);
        actual.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected, customMessage);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeNegative(this double actual, string? customMessage = null)
    {
        var expected = default(double);
        actual.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected, customMessage);
    }

    /// <summary>
    /// float
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBePositive(this float actual, string? customMessage = null)
    {
        var expected = default(float);
        actual.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected, customMessage);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeNegative(this float actual, string? customMessage = null)
    {
        var expected = default(float);
        actual.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected, customMessage);
    }

    /// <summary>
    /// int
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBePositive(this int actual, string? customMessage = null)
    {
        var expected = default(int);
        actual.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected, customMessage);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeNegative(this int actual, string? customMessage = null)
    {
        var expected = default(int);
        actual.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected, customMessage);
    }

    /// <summary>
    /// long
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBePositive(this long actual, string? customMessage = null)
    {
        var expected = default(long);
        actual.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected, customMessage);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeNegative(this long actual, string? customMessage = null)
    {
        var expected = default(long);
        actual.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected, customMessage);
    }

    /// <summary>
    /// short
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBePositive(this short actual, string? customMessage = null)
    {
        var expected = default(short);
        actual.AssertAwesomely(v => Is.GreaterThan(v, expected), actual, expected, customMessage);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeNegative(this short actual, string? customMessage = null)
    {
        var expected = default(short);
        actual.AssertAwesomely(v => Is.LessThan(v, expected), actual, expected, customMessage);
    }
}