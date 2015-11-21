using Shouldly.DifferenceHighlighting2;
using Shouldly.Internals.Assertions;
using System;

namespace Shouldly.Internals.AssertionFactories
{
    internal static class StringShouldBeAssertionFactory
    {
        public static IAssertion Create(string expected, string actual, StringCompareShould options)
        {
            switch (options)
            {
                case 0:
                    return new StringShouldBeAssertion(
                        expected, actual,
                        StringComparer.InvariantCulture.Equals,
                        new ActualCodeTextGetter(),
                        new StringDifferenceHighlighter(
                            "Case and Line Ending Sensitive Comparison",
                            Case.Sensitive));
                case StringCompareShould.IgnoreCase:
                    return new StringShouldBeAssertion(
                        expected, actual,
                        StringComparer.InvariantCultureIgnoreCase.Equals,
                        new ActualCodeTextGetter(),
                        new StringDifferenceHighlighter(
                            "Case Insensitive and Line Ending Sensitive Comparison",
                            Case.Insensitive));
                case StringCompareShould.IgnoreLineEndings:
                    return new StringShouldBeAssertion(
                        expected, actual,
                        (a, e) => StringComparer.InvariantCulture.Equals(
                            a.NormalizeLineEndings(), e.NormalizeLineEndings()),
                        new ActualCodeTextGetter(),
                        new StringDifferenceHighlighter(
                            "Case Sensitive and Line Ending Insensitive Comparison",
                            Case.Sensitive, s => s.NormalizeLineEndings()));
                case StringCompareShould.IgnoreCase | StringCompareShould.IgnoreLineEndings:
                    return new StringShouldBeAssertion(
                        expected, actual,
                        (a, e) => StringComparer.InvariantCultureIgnoreCase.Equals(
                            a.NormalizeLineEndings(), e.NormalizeLineEndings()),
                        new ActualCodeTextGetter(),
                        new StringDifferenceHighlighter(
                            "Case and Line Ending Insensitive Comparison",
                            Case.Insensitive, s => s.NormalizeLineEndings()));
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
