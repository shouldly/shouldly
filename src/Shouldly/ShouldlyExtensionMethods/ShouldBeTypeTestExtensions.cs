using NotNullAttribute = System.Diagnostics.CodeAnalysis.NotNullAttribute;
using Shouldly.Internals.Failures;

namespace Shouldly;

public static partial class ShouldBeTestExtensions
{
    /// <summary>
    /// Asserts that the actual object is assignable to the type <typeparamref name="T"/>.
    /// </summary>
    [return: NotNullIfNotNull(nameof(actual))]
    public static T? ShouldBeAssignableTo<T>(this object? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        ShouldBeAssignableTo(actual, typeof(T), customMessage, actualExpression);
        return (T?)actual;
    }

    /// <summary>
    /// Asserts that the actual object is assignable to the specified type.
    /// </summary>
    public static void ShouldBeAssignableTo(this object? actual, Type expected, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (!actual.Satisfies(v =>
        {
            if (v == null)
            {
                return !expected.IsValueType ||
                       (expected.IsGenericType && expected.GetGenericTypeDefinition() == typeof(Nullable<>));
            }

            return expected.IsInstanceOfType(v);
        }))
            throw Fail.ShouldBeAssignableTo.With(expected).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the actual object is exactly of the type <typeparamref name="T"/>.
    /// </summary>
    public static T ShouldBeOfType<T>([NotNull] this object? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        ShouldBeOfType(actual, typeof(T), customMessage, actualExpression);
        return (T)actual;
    }

    /// <summary>
    /// Asserts that the actual object is exactly of the specified type.
    /// </summary>
    public static void ShouldBeOfType([NotNull] this object? actual, Type expected, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (!actual.Satisfies(v => v != null && v.GetType() == expected))
            throw Fail.ShouldBeOfType.With(expected).For(actual, customMessage, actualExpression);
        Debug.Assert(actual != null);
    }

    /// <summary>
    /// Asserts that the actual object is not assignable to the type <typeparamref name="T"/>.
    /// </summary>
    public static void ShouldNotBeAssignableTo<T>(this object? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        ShouldNotBeAssignableTo(actual, typeof(T), customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the actual object is not assignable to the specified type.
    /// </summary>
    public static void ShouldNotBeAssignableTo(this object? actual, Type expected, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (!actual.Satisfies(v => !expected.IsInstanceOfType(v)))
            throw Fail.ShouldNotBeAssignableTo.With(expected).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the actual object is not exactly of the type <typeparamref name="T"/>.
    /// </summary>
    public static void ShouldNotBeOfType<T>(this object? actual, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        ShouldNotBeOfType(actual, typeof(T), customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the actual object is not exactly of the specified type.
    /// </summary>
    public static void ShouldNotBeOfType(this object? actual, Type expected, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        if (!actual.Satisfies(v => v == null || v.GetType() != expected))
            throw Fail.ShouldNotBeOfType.With(expected).For(actual, customMessage, actualExpression);
    }
}
