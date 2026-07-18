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
        actual.AssertAwesomely(
            v => SpanSequenceEqual(v.Span, expected.Span),
            actual, expected, customMessage, actualExpression: actualExpression);
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
        actual.AssertAwesomely(
            v => SpanSequenceEqual(v.Span, expected.Span),
            actual, expected, customMessage, actualExpression: actualExpression);
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
        actual.AssertAwesomely(
            v => !SpanSequenceEqual(v.Span, expected.Span),
            actual, expected, customMessage, actualExpression: actualExpression);
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
        actual.AssertAwesomely(
            v => !SpanSequenceEqual(v.Span, expected.Span),
            actual, expected, customMessage, actualExpression: actualExpression);
    }

    // Walks two spans element-wise via Shouldly's own equality comparer so the
    // element comparison stays consistent with ShouldBe on any other collection
    // (numerics, IEquatable, IComparable, etc.). Avoids the per-call array
    // allocation a ToArray()+SequenceEqual path would incur; value-type elements
    // are still boxed inside the comparer's object-based numeric/reference checks.
    private static bool SpanSequenceEqual<T>(ReadOnlySpan<T> a, ReadOnlySpan<T> b)
    {
        if (a.Length != b.Length) return false;

#if NET8_0_OR_GREATER
        // Fast path for integral primitives: bitwise equality matches value
        // equality (no NaN/-0.0 quirks like float/double), so we can defer to
        // the vectorised BCL comparison and skip both the comparer allocation
        // and the per-element boxing inside Internals.EqualityComparer<T>.
        // The typeof(T) chain folds to a constant per generic instantiation.
        // Gated on net8+: the netstandard2.0 SequenceEqual<T> overload
        // constrains T to IEquatable<T>.
        if (typeof(T) == typeof(byte) || typeof(T) == typeof(sbyte) ||
            typeof(T) == typeof(short) || typeof(T) == typeof(ushort) ||
            typeof(T) == typeof(int) || typeof(T) == typeof(uint) ||
            typeof(T) == typeof(long) || typeof(T) == typeof(ulong) ||
            typeof(T) == typeof(char))
        {
            return a.SequenceEqual(b);
        }
#endif

        IEqualityComparer<T> comparer = new Internals.EqualityComparer<T>();
        for (var i = 0; i < a.Length; i++)
        {
            if (!comparer.Equals(a[i], b[i]))
                return false;
        }

        return true;
    }
}
