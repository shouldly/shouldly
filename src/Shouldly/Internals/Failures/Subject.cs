namespace Shouldly.Internals.Failures;

/// <summary>
/// Resolves the subject of a failure message: the source text of the actual value's expression at the call site.
/// </summary>
static class Subject
{
    /// <summary>
    /// The subject for a failing assertion. The caller-argument expression is used when the assertion captured one.
    /// Without one, the armed trip-wire throws; otherwise the subject falls back to source recovered by walking the
    /// stack (netstandard2.0 only, unless disabled) and finally to the formatted <paramref name="actual"/>.
    /// </summary>
    /// <param name="assertion">The assertion's name, for the trip-wire's message.</param>
    /// <param name="actual">The value under test.</param>
    /// <param name="actualExpression">The expression captured by <c>[CallerArgumentExpression]</c>, if any.</param>
    /// <param name="stackTrace">A stack trace captured before an <c>await</c>, for the stack-walking fallback.</param>
    public static string? Resolve(string assertion, object? actual, string? actualExpression, StackTrace? stackTrace = null)
    {
        if (actualExpression != null)
            return actualExpression;

        if (ShouldlyConfiguration.IsCallerArgumentExpressionRequired())
            throw TripWireException(assertion);

#if NETSTANDARD2_0
        return ShouldlyConfiguration.IsSourceDisabledInErrors()
            ? actual.ToStringAwesomely()
            : new ActualCodeTextGetter().GetCodeText(actual, stackTrace);
#else
        return actual.ToStringAwesomely();
#endif
    }

    private static InvalidOperationException TripWireException(string assertion) =>
        new(
            $"Assertion '{assertion}' fell back to stack-trace parsing despite the " +
            $"{nameof(ShouldlyConfiguration.AssertCallerArgumentExpressionIsUsed)} trip-wire being armed. " +
            "Either the public method is missing its [CallerArgumentExpression] parameter, the captured " +
            "value is not threaded through to the message constructor, or the call site cannot use CAE " +
            $"(e.g. dynamic dispatch, obsolete params overloads) — wrap such call sites in " +
            $"{nameof(ShouldlyConfiguration)}.{nameof(ShouldlyConfiguration.AllowStackWalking)}() to opt out.");
}
