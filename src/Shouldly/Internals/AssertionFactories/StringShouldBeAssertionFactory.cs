using Shouldly.DifferenceHighlighting;
using Shouldly.Internals.Assertions;

namespace Shouldly.Internals.AssertionFactories;

static class StringShouldBeAssertionFactory
{
    /// <summary>
    /// Pass <paramref name="useEqualityRules"/> as <c>true</c> from <c>ShouldBe</c>, so that without options a string
    /// equality rule (<see cref="ShouldlyConfiguration.UseEquality{T}(Func{T, T, bool})"/>) decides, as it does for
    /// <c>ShouldNotBe</c>. Options the caller asked for take precedence over a rule.
    /// </summary>
    public static IAssertion Create(string? expected, string? actual, StringCompareShould options, [CallerMemberName] string shouldlyMethod = null!, string? actualExpression = null, bool useEqualityRules = false)
    {
        var optionsList = new List<string>();
        if ((options & StringCompareShould.IgnoreLineEndings) != 0)
        {
            expected = expected.NormalizeLineEndings();
            actual = actual.NormalizeLineEndings();
            optionsList.Add("Ignoring line endings");
        }

        Case sensitivity;
        Func<string?, string?, bool> stringComparer;
        if (useEqualityRules && options == 0 && !EqualityRules.IsEmpty && EqualityRules.CouldApplyTo(typeof(string)))
        {
            sensitivity = Case.Sensitive;
            stringComparer = (actualValue, expectedValue) => Equality.Equal(actualValue, expectedValue);
        }
        else if ((options & StringCompareShould.IgnoreCase) == 0)
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
            expected,
            actual,
            stringComparer,
            new ActualCodeTextGetter(),
            new StringDifferenceHighlighter(sensitivity),
            string.Join(", ", optionsList.ToArray()),
            shouldlyMethod.PascalToSpaced(),
            actualExpression);
    }
}