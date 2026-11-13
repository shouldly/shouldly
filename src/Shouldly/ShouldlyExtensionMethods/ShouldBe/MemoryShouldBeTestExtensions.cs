using Shouldly.Internals.Failures;

namespace Shouldly;

public static partial class ShouldBeTestExtensions
{
    /// <summary>
    /// Asserts that a <see cref="Memory{T}"/> has content-equal elements to another <see cref="Memory{T}"/>.
    /// </summary>
    public static void ShouldBe<T>(
        this Memory<T> actual,
        Memory<T> expected,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var equality = Internals.Equality.OfMemories(Internals.Equality<T>.Default);
        if (!actual.Satisfies(v => equality.Equal(v, expected)))
            throw Fail.ShouldBe.With(expected, equality).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that a <see cref="ReadOnlyMemory{T}"/> has content-equal elements to another <see cref="ReadOnlyMemory{T}"/>.
    /// </summary>
    public static void ShouldBe<T>(
        this ReadOnlyMemory<T> actual,
        ReadOnlyMemory<T> expected,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var equality = Internals.Equality.OfReadOnlyMemories(Internals.Equality<T>.Default);
        if (!actual.Satisfies(v => equality.Equal(v, expected)))
            throw Fail.ShouldBe.With(expected, equality).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that a <see cref="Memory{T}"/> does not have content-equal elements to another <see cref="Memory{T}"/>.
    /// </summary>
    public static void ShouldNotBe<T>(
        this Memory<T> actual,
        Memory<T> expected,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var equality = Internals.Equality.OfMemories(Internals.Equality<T>.Default);
        if (!actual.Satisfies(v => !equality.Equal(v, expected)))
            throw Fail.ShouldNotBe.With(expected, equality).For(actual, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that a <see cref="ReadOnlyMemory{T}"/> does not have content-equal elements to another <see cref="ReadOnlyMemory{T}"/>.
    /// </summary>
    public static void ShouldNotBe<T>(
        this ReadOnlyMemory<T> actual,
        ReadOnlyMemory<T> expected,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var equality = Internals.Equality.OfReadOnlyMemories(Internals.Equality<T>.Default);
        if (!actual.Satisfies(v => !equality.Equal(v, expected)))
            throw Fail.ShouldNotBe.With(expected, equality).For(actual, customMessage, actualExpression);
    }
}
