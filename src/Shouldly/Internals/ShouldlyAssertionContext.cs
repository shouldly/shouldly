using Shouldly.Internals;

namespace Shouldly;

/// <summary>
/// Implementation of the Shouldly assertion context
/// </summary>
public class ShouldlyAssertionContext : IShouldlyAssertionContext
{
    /// <inheritdoc/>
    public string ShouldMethod { get; set; }

    /// <inheritdoc/>
    public string? CodePart { get; set; }

    /// <inheritdoc/>
    public string? FileName { get; set; }

    /// <inheritdoc/>
    public int? LineNumber { get; set; }

    /// <inheritdoc/>
    public object? Key { get; set; }

    /// <inheritdoc/>
    public object? Expected { get; set; }

    /// <inheritdoc/>
    public object? Actual { get; set; }

    /// <inheritdoc/>
    public object? Tolerance { get; set; }

    /// <inheritdoc/>
    public Case? CaseSensitivity { get; set; }

    /// <inheritdoc/>
    public bool CodePartMatchesActual => CodePart == Actual.ToStringAwesomely();

    /// <inheritdoc/>
    public TimeSpan? Timeout { get; set; }

    /// <inheritdoc/>
    public bool IgnoreOrder { get; set; }

    // For now, this property cannot just check to see if "Actual != null". The term is overloaded.
    // In some cases it means the "Actual" value is not relevant (eg: "dictionary.ContainsKey(key)") and in some
    // cases it means that the value is relevant, but during execution we got a null. (eg: Foo.ShouldBe(bar) where
    // Foo is null). So for now, it is a flag needs to be set externally to determine whether or not the "Actual" value
    // is relevant.
    /// <inheritdoc/>
    public bool HasRelevantActual { get; set; }

    /// <inheritdoc/>
    public bool HasRelevantKey { get; set; }

    /// <inheritdoc/>
    public bool IsNegatedAssertion => ShouldMethod.Contains("Not");

    /// <inheritdoc/>
    public string? CustomMessage { get; set; }

    /// <inheritdoc/>
    public Expression? Filter { get; set; }

    /// <inheritdoc/>
    public int? MatchCount { get; set; }

    /// <inheritdoc/>
    public SortDirection SortDirection { get; set; }

    /// <inheritdoc/>
    public int OutOfOrderIndex { get; set; }

    /// <inheritdoc/>
    public object? OutOfOrderObject { get; set; }

    /// <inheritdoc/>
    public IEnumerable<string>? Path { get; set; }

    /// <summary>
    /// Initializes a new instance of the ShouldlyAssertionContext class
    /// </summary>
    /// <param name="shouldlyMethod">The name of the should method being called</param>
    /// <param name="expected">The expected value</param>
    /// <param name="actual">The actual value</param>
    /// <param name="stackTrace">The stack trace</param>
    /// <param name="actualExpression">The source-level expression of the actual argument, if captured at the call site via <see cref="CallerArgumentExpressionAttribute"/>. When provided, supersedes stack-trace parsing.</param>
    public ShouldlyAssertionContext(
        string shouldlyMethod,
        object? expected = null,
        object? actual = null,
        StackTrace? stackTrace = null,
        string? actualExpression = null)
    {
        Expected = expected;
        Actual = actual;
        ShouldMethod = shouldlyMethod;

        if (actualExpression != null && !ShouldlyConfiguration.IsSourceDisabledInErrors())
        {
            CodePart = actualExpression;
        }
        else
        {
            if (ShouldlyConfiguration.IsCallerArgumentExpressionRequired())
                throw TripWireException(shouldlyMethod);

#if NETSTANDARD2_0
            var actualCodeGetter = new ActualCodeTextGetter();
            CodePart = actualCodeGetter.GetCodeText(actual, stackTrace);
            FileName = actualCodeGetter.FileName;
            LineNumber = actualCodeGetter.LineNumber;
#else
            // On net8.0+ we don't walk the stack to recover source text — callers
            // are expected to supply CallerArgumentExpression. When they don't (or
            // when source is suppressed via configuration), fall back to a value
            // formatted from the actual argument so messages remain coherent.
            CodePart = actual.ToStringAwesomely();
#endif
        }
    }

    private static InvalidOperationException TripWireException(string shouldlyMethod) =>
        new(
            $"Assertion '{shouldlyMethod}' fell back to stack-trace parsing despite the " +
            $"{nameof(ShouldlyConfiguration.AssertCallerArgumentExpressionIsUsed)} trip-wire being armed. " +
            "Either the public method is missing its [CallerArgumentExpression] parameter, the captured " +
            "value is not threaded through to the message constructor, or the call site cannot use CAE " +
            $"(e.g. dynamic dispatch, obsolete params overloads) — wrap such call sites in " +
            $"{nameof(ShouldlyConfiguration)}.{nameof(ShouldlyConfiguration.AllowStackWalking)}() to opt out.");
}