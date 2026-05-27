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
        var _actualTrimmed = Trim(_actual);
        var _expectedTrimmed = Trim(_expected);
        string? codeText;
        if (_actualExpression != null && !ShouldlyConfiguration.IsSourceDisabledInErrors())
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

#if NETSTANDARD2_0
            codeText = _codeTextGetter.GetCodeText(_actual);
#else
            codeText = _actual.ToStringAwesomely();
#endif
        }
        var withOption = string.IsNullOrEmpty(_options) ? null : $" with options: {_options}";
        var actualValue = _actualTrimmed.ToStringAwesomely();
        var expectedValue = _expectedTrimmed.ToStringAwesomely();

        var differences = _diffHighlighter.HighlightDifferences(_expectedTrimmed, _actualTrimmed);

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

    private static string? Trim(string? value)
    {
        if (value == null)
        {
            return null;
        }

        if (value.Length <= 5000)
        {
            return value;
        }

        return value[..5000];
    }

    public bool IsSatisfied() =>
        _compare(_actual, _expected);
}