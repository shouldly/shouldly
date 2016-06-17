using Shouldly.Internals.Assertions;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Shouldly.DifferenceHighlighting;

namespace Shouldly.Internals.AssertionFactories
{
    internal static class StringShouldBeAssertionFactory
    {
        public static IAssertion Create(string expected, string actual, StringCompareShould options, [CallerMemberName] string shouldlyMethod = null)
        {
            List<string> optionsList = new List<string>();
            if ((options & StringCompareShould.IgnoreLineEndings) != 0)
            {
                expected = expected.NormalizeLineEndings();
                actual = actual.NormalizeLineEndings();
                optionsList.Add("Ignoring line endings");
            }

            Case sensitivity;
            Func<string, string, bool> stringComparer;
            if ((options & StringCompareShould.IgnoreCase) == 0)
            {
                sensitivity = Case.Sensitive;
                stringComparer = StringComparer.Ordinal.Equals;
            }
            else
            {
                sensitivity = Case.Insensitive;
                stringComparer = StringComparer.OrdinalIgnoreCase.Equals;
                optionsList.Add("Ignoring case");
            }

            return new StringShouldBeAssertion(
                expected, actual,
                stringComparer,
                new ActualCodeTextGetter(),
                new StringDifferenceHighlighter(sensitivity),
                string.Join(", ", optionsList.ToArray()),
                shouldlyMethod.PascalToSpaced());
        }
    }
}
