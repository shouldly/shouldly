using Shouldly.DifferenceHighlighting;

namespace Shouldly.Internals.Assertions;

class StringShouldBeAssertion : IAssertion
{
    private readonly string? _expected;
    private readonly string? _actual;
    private readonly Func<string?, string?, bool> _compare;
    private readonly ICodeTextGetter _codeTextGetter;
    private readonly IStringDifferenceHighlighter _diffHighlighter;
    private readonly string _options;
    private readonly string _shouldlyMethod;
    private readonly string? _actualExpression;

    public StringShouldBeAssertion(
        string? expected,
        string? actual,
        Func<string?, string?, bool> compare,
        ICodeTextGetter codeTextGetter,
        IStringDifferenceHighlighter diffHighlighter,
        string options,
        string shouldlyMethod,
        string? actualExpression = null)
    {
        _expected = expected;
        _actual = actual;
        _compare = compare;
        _codeTextGetter = codeTextGetter;
        _diffHighlighter = diffHighlighter;
        _options = options;
        _shouldlyMethod = shouldlyMethod;
        _actualExpression = actualExpression;
    }

    public string GenerateMessage(string? customMessage)
    {
        var actualValue = Echo(_actual);
        var expectedValue = Echo(_expected);

        string? codeText;
        if (_actualExpression != null)
        {
            codeText = _actualExpression;
        }
        else
        {
            if (ShouldlyConfiguration.IsCallerArgumentExpressionRequired())
                throw new InvalidOperationException(
                    $"String assertion '{_shouldlyMethod}' fell back to stack-trace parsing despite the " +
                    $"{nameof(ShouldlyConfiguration.AssertCallerArgumentExpressionIsUsed)} trip-wire being armed. " +
                    $"Wrap the call site in {nameof(ShouldlyConfiguration)}.{nameof(ShouldlyConfiguration.AllowStackWalking)}() to opt out.");

            // No call-site text to show, so the rendered value stands in for it. Using the same
            // echo means the "but was" line below collapses to " not" instead of repeating it.
#if NETSTANDARD2_0
            codeText = ShouldlyConfiguration.IsSourceDisabledInErrors()
                ? actualValue
                : _codeTextGetter.GetCodeText(_actual);
#else
            codeText = actualValue;
#endif
        }
        var withOption = string.IsNullOrEmpty(_options) ? null : $" with options: {_options}";

        // Differences come from the full values. Truncating first would let a difference that
        // sits past the echo budget disappear from the message entirely.
        var differences = _diffHighlighter.HighlightDifferences(_expected, _actual);

        var actual = codeText == actualValue ?
            " not" :
            $"""

             {actualValue}
             """;
        var message =
            $"""
             {codeText}
                 {_shouldlyMethod}{withOption}
             {expectedValue}
                 but was{actual}
             """;

        if (differences != null)
        {
            message +=
                $"""
                 
                     difference
                 {differences}
                 """;
        }

        if (customMessage != null)
        {
            message +=
                $"""


                 Additional Info:
                     {customMessage}
                 """;
        }

        return message;
    }

    // Renders a value for the "should be X but was Y" lines, clipped to
    // ShouldlyConfiguration.MaxStringLengthInMessages. Truncation is always announced —
    // silently swallowing the tail leaves no clue that the printed value is partial.
    private static string? Echo(string? value)
    {
        var limit = ShouldlyConfiguration.MaxStringLengthInMessages;

        if (value == null || value.Length <= limit)
        {
            return value.ToStringAwesomely();
        }

        return $"{value[..limit].ToStringAwesomely()} " +
               $"(truncated to {limit} of {value.Length} characters, see " +
               $"{nameof(ShouldlyConfiguration)}.{nameof(ShouldlyConfiguration.MaxStringLengthInMessages)})";
    }

    public bool IsSatisfied() =>
        _compare(_actual, _expected);
}