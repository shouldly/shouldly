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
            throw Fail.ShouldBe.With(expected).For(actual, customMessage, actualExpression);
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
            throw Fail.ShouldBe.With(expected).For(actual, customMessage, actualExpression);
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
            throw Fail.ShouldNotBe.With(expected).For(actual, customMessage, actualExpression);
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
            throw Fail.ShouldNotBe.With(expected).For(actual, customMessage, actualExpression);
    }

    // A rule registered for the memory type itself decides first, as it does when the memories are compared as
    // elements of a sequence; otherwise the contents are compared element by element.
    private static bool MemoryEqual<TMemory, T>(TMemory actual, TMemory expected, ReadOnlySpan<T> actualSpan, ReadOnlySpan<T> expectedSpan)
        where TMemory : struct
    {
        if (!Internals.EqualityRules.IsEmpty &&
            Internals.EqualityRules.TryEquals(expected, actual, out var ruleResult))
            return ruleResult;

        return SpanSequenceEqual(actualSpan, expectedSpan);
    }

    // Walks two spans element-wise via Shouldly's own equality comparer so the
    // element comparison stays consistent with ShouldBe on any other collection
    // (numerics, IEquatable, IComparable, etc.). Avoids the per-call array
    // allocation a ToArray()+SequenceEqual path would incur; value-type elements
    // are still boxed inside the comparer's object-based numeric/reference checks.
    private static bool SpanSequenceEqual<T>(ReadOnlySpan<T> actual, ReadOnlySpan<T> expected)
    {
        if (actual.Length != expected.Length) return false;

#if NET8_0_OR_GREATER
        // Fast path for integral primitives: bitwise equality matches value
        // equality (no NaN/-0.0 quirks like float/double), so we can defer to
        // the vectorised BCL comparison and skip both the comparer allocation
        // and the per-element boxing inside Internals.EqualityComparer<T>.
        // The typeof(T) chain folds to a constant per generic instantiation.
        // Gated on net8+: the netstandard2.0 SequenceEqual<T> overload
        // constrains T to IEquatable<T>. Skipped when an equality rule could
        // apply to T, which only the comparer below consults.
        if ((Internals.EqualityRules.IsEmpty || !Internals.EqualityRules.CouldApplyTo(typeof(T))) &&
            (typeof(T) == typeof(byte) || typeof(T) == typeof(sbyte) ||
            typeof(T) == typeof(short) || typeof(T) == typeof(ushort) ||
            typeof(T) == typeof(int) || typeof(T) == typeof(uint) ||
            typeof(T) == typeof(long) || typeof(T) == typeof(ulong) ||
            typeof(T) == typeof(char)))
        {
            return actual.SequenceEqual(expected);
        }
#endif

        var comparer = Internals.Equality.ComparerFor<T>();
        for (var i = 0; i < actual.Length; i++)
        {
            // Equals is called on the expected element, as everywhere else in ShouldBe (see Equality.Equal).
            if (!Internals.Equality.Equal(actual[i], expected[i], comparer))
                return false;
        }

        return true;
    }
}
