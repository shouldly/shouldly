using System;
using Shouldly.DifferenceHighlighting;

namespace Shouldly.Internals.Assertions
{
    internal class StringShouldBeAssertion : IAssertion
    {
        readonly string _expected;
        readonly string _actual;
        readonly Func<string, string, bool> _compare;
        readonly ICodeTextGetter _codeTextGetter;
        readonly IStringDifferenceHighlighter _diffHighlighter;
        readonly string _options;
        readonly string _shouldlyMethod;

        public StringShouldBeAssertion(
            string expected, string actual,
            Func<string, string, bool> compare,
            ICodeTextGetter codeTextGetter,
            IStringDifferenceHighlighter diffHighlighter,
            string options,
            string shouldlyMethod)
        {
            _expected = expected;
            _actual = actual;
            _compare = compare;
            _codeTextGetter = codeTextGetter;
            _diffHighlighter = diffHighlighter;
            _options = options;
            _shouldlyMethod = shouldlyMethod;
        }

        public string GenerateMessage(string customMessage)
        {
            var codeText = _codeTextGetter.GetCodeText(_actual);
            var withOption = string.IsNullOrEmpty(_options) ? null : $" with options: {_options}";
            var actualValue = _actual.ToStringAwesomely();
            var expectedValue = _expected.ToStringAwesomely();

            var actual = codeText == actualValue ? " not" : $@"
{actualValue}";
            var message =
$@"{codeText}
    {_shouldlyMethod}{withOption}
{expectedValue}
    but was{actual}";
            if (!ShouldlyConfiguration.IsDifferenceHighlightingDisabled())
            {
                message += $@"difference
                {_diffHighlighter.HighlightDifferences(_expected, _actual)}";                
            }

            if (customMessage != null)
            {
                message += $@"

Additional Info:
    {customMessage}";
            }
            return message;
        }

        public bool IsSatisfied()
        {
            return _compare(_actual, _expected);
        }
    }
}
