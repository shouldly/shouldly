﻿using Shouldly.DifferenceHighlighting2;
using System;

namespace Shouldly.Internals.Assertions
{
    internal class StringShouldBeAssertion : IAssertion
    {
        readonly string _expected;
        readonly string _actual;
        readonly Func<string, string, bool> _compare;
        readonly ICodeTextGetter _codeTextGetter;
        readonly IStringDifferenceHighlighter _diffHighlighter;

        public StringShouldBeAssertion(
            string expected, string actual,
            Func<string, string, bool> compare,
            ICodeTextGetter codeTextGetter,
            IStringDifferenceHighlighter diffHighlighter)
        {
            _expected = expected;
            _actual = actual;
            _compare = compare;
            _codeTextGetter = codeTextGetter;
            _diffHighlighter = diffHighlighter;
        }

        public string GenerateMessage(string customMessage)
        {
            var message = string.Format(
@"    {0}
        {1}
    {2}
        but was
    {3}
        difference
    {4}",
            _codeTextGetter.GetCodeText(),
            "should be",
            _expected.ToStringAwesomely(),
            _actual.ToStringAwesomely(),
            _diffHighlighter.HighlightDifferences(_expected, _actual));

            if (customMessage != null)
            {
                message += string.Format(@"

Additional Info:
    {0}", customMessage);
            }
            return message;
        }

        public bool IsSatisfied()
        {
            return _compare(_actual, _expected);
        }
    }
}
