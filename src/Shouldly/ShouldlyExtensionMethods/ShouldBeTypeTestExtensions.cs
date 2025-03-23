﻿using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;

namespace Shouldly;

public static partial class ShouldBeTestExtensions
{
    /// <summary>
    /// Asserts that the actual object is assignable to the type <typeparamref name="T"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    [return: NotNullIfNotNull(nameof(actual))]
    public static T? ShouldBeAssignableTo<T>(this object? actual, string? customMessage = null)
    {
        ShouldBeAssignableTo(actual, typeof(T), customMessage);
        return (T?)actual;
    }

    /// <summary>
    /// Asserts that the actual object is assignable to the specified type.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeAssignableTo(this object? actual, Type expected, string? customMessage = null)
    {
        actual.AssertAwesomely(v =>
        {
            if (v == null)
            {
                return !expected.IsValueType ||
                       (expected.IsGenericType && expected.GetGenericTypeDefinition() == typeof(Nullable<>));
            }

            return expected.IsInstanceOfType(v);
        }, actual, expected, customMessage);
    }

    /// <summary>
    /// Asserts that the actual object is exactly of the type <typeparamref name="T"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T ShouldBeOfType<T>([NotNull] this object? actual, string? customMessage = null)
    {
        ShouldBeOfType(actual, typeof(T), customMessage);
        return (T)actual;
    }

    /// <summary>
    /// Asserts that the actual object is exactly of the specified type.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldBeOfType([NotNull] this object? actual, Type expected, string? customMessage = null)
    {
        actual.AssertAwesomely(v => v != null && v.GetType() == expected, actual, expected, customMessage);
        Debug.Assert(actual != null);
    }

    /// <summary>
    /// Asserts that the actual object is not assignable to the type <typeparamref name="T"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotBeAssignableTo<T>(this object? actual, string? customMessage = null)
    {
        ShouldNotBeAssignableTo(actual, typeof(T), customMessage);
    }

    /// <summary>
    /// Asserts that the actual object is not assignable to the specified type.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotBeAssignableTo(this object? actual, Type expected, string? customMessage = null)
    {
        actual.AssertAwesomely(v => !expected.IsInstanceOfType(v), actual, expected, customMessage);
    }

    /// <summary>
    /// Asserts that the actual object is not exactly of the type <typeparamref name="T"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotBeOfType<T>(this object? actual, string? customMessage = null)
    {
        ShouldNotBeOfType(actual, typeof(T), customMessage);
    }

    /// <summary>
    /// Asserts that the actual object is not exactly of the specified type.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldNotBeOfType(this object? actual, Type expected, string? customMessage = null)
    {
        actual.AssertAwesomely(v => v == null || v.GetType() != expected, actual, expected, customMessage);
    }
}
