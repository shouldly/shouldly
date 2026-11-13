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
        if (!actual.Satisfies(v => MemoryEqual(v, expected, v.Span, expected.Span)))
            throw Fail.ShouldBe.With(expected, Internals.Equality<Memory<T>>.Default).For(actual, customMessage, actualExpression);
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
        if (!actual.Satisfies(v => MemoryEqual(v, expected, v.Span, expected.Span)))
            throw Fail.ShouldBe.With(expected, Internals.Equality<ReadOnlyMemory<T>>.Default).For(actual, customMessage, actualExpression);
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
        if (!actual.Satisfies(v => !MemoryEqual(v, expected, v.Span, expected.Span)))
            throw Fail.ShouldNotBe.With(expected, Internals.Equality<Memory<T>>.Default).For(actual, customMessage, actualExpression);
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
        if (!actual.Satisfies(v => !MemoryEqual(v, expected, v.Span, expected.Span)))
            throw Fail.ShouldNotBe.With(expected, Internals.Equality<ReadOnlyMemory<T>>.Default).For(actual, customMessage, actualExpression);
    }

    // A rule registered for the memory type itself decides first, as it does when the memories are compared as
    // elements of a sequence; otherwise the contents are compared element by element.
    private static bool MemoryEqual<TMemory, T>(TMemory actual, TMemory expected, ReadOnlySpan<T> actualSpan, ReadOnlySpan<T> expectedSpan)
        where TMemory : struct
    {
        if (!Internals.EqualityRules.IsEmpty &&
            Internals.EqualityRules.TryEquals(expected, actual, out var ruleResult))
            return ruleResult;

        return Internals.Equality<T>.Default.MatchInOrder(actualSpan, expectedSpan);
    }
}
