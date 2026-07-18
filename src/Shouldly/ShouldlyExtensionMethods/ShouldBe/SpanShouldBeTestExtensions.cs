namespace Shouldly;

public static partial class ShouldBeTestExtensions
{
    // These overloads are marked as the lowest overload-resolution priority on
    // purpose. From C# 14, arrays and strings gain an implicit span conversion
    // for extension-method receivers, so `someArray.ShouldBe([...])` would
    // otherwise become ambiguous between the generic T / IEnumerable<T> overloads
    // and these span ones. Demoting the span overloads means a genuine span
    // (which the generic T overload can't bind, since ref structs can't be a type
    // argument) still selects them, while arrays/strings keep their existing
    // higher-priority overloads and behaviour.

    /// <summary>
    /// Asserts that a <see cref="ReadOnlySpan{T}"/> has content-equal elements to another <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    [OverloadResolutionPriority(-1)]
    public static void ShouldBe<T>(
        this ReadOnlySpan<T> actual,
        ReadOnlySpan<T> expected,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        AssertSpansEqual(actual, expected, shouldBeEqual: true, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that a <see cref="Span{T}"/> has content-equal elements to another <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    [OverloadResolutionPriority(-1)]
    public static void ShouldBe<T>(
        this Span<T> actual,
        ReadOnlySpan<T> expected,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        AssertSpansEqual(actual, expected, shouldBeEqual: true, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that a <see cref="ReadOnlySpan{T}"/> does not have content-equal elements to another <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    [OverloadResolutionPriority(-1)]
    public static void ShouldNotBe<T>(
        this ReadOnlySpan<T> actual,
        ReadOnlySpan<T> expected,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        AssertSpansEqual(actual, expected, shouldBeEqual: false, customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that a <see cref="Span{T}"/> does not have content-equal elements to another <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    [OverloadResolutionPriority(-1)]
    public static void ShouldNotBe<T>(
        this Span<T> actual,
        ReadOnlySpan<T> expected,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        AssertSpansEqual(actual, expected, shouldBeEqual: false, customMessage, actualExpression);
    }

    // Spans are ref structs: they cannot be boxed into object?, captured in the
    // Func<T,bool> AssertAwesomely expects, or stored on the assertion context.
    // So we compare inline and only materialise (ToArray) on the failure path,
    // where we're allocating to build the message anyway. CallerMemberName picks
    // up "ShouldBe"/"ShouldNotBe" from the public overload that called us, so the
    // message generator negates correctly.
    private static void AssertSpansEqual<T>(
        ReadOnlySpan<T> actual,
        ReadOnlySpan<T> expected,
        bool shouldBeEqual,
        string? customMessage,
        string? actualExpression,
        [CallerMemberName] string shouldlyMethod = null!)
    {
        if (SpanSequenceEqual(actual, expected) == shouldBeEqual)
            return;

        throw new ShouldAssertException(
            new ExpectedActualShouldlyMessage(expected.ToArray(), actual.ToArray(), customMessage, shouldlyMethod, actualExpression).ToString());
    }
}
