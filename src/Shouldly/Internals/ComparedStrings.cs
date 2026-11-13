namespace Shouldly.Internals;

/// <summary>
/// Two strings as an assertion compares them under <see cref="StringCompareShould"/> options: normalized as the
/// options ask, with the comparison those options select.
/// </summary>
readonly struct ComparedStrings
{
    private readonly Func<string?, string?, bool> _equal;

    private ComparedStrings(string? actual, string? expected, string options, Case sensitivity, Func<string?, string?, bool> equal)
    {
        Actual = actual;
        Expected = expected;
        Options = options;
        Sensitivity = sensitivity;
        _equal = equal;
    }

    /// <summary>The actual string, as compared.</summary>
    public string? Actual { get; }

    /// <summary>The expected string, as compared.</summary>
    public string? Expected { get; }

    /// <summary>The options in effect, described for a failure message. Empty when there are none.</summary>
    public string Options { get; }

    /// <summary>Whether case counted, which difference highlighting follows.</summary>
    public Case Sensitivity { get; }

    /// <summary>Whether the two strings are equal under the options.</summary>
    public bool AreEqual() => _equal(Actual, Expected);

    /// <param name="actual">The actual string.</param>
    /// <param name="expected">The expected string.</param>
    /// <param name="options">How to compare them.</param>
    /// <param name="useEqualityRules">Whether a registered equality rule for strings decides when no option is set.</param>
    public static ComparedStrings Under(StringCompareShould options, string? actual, string? expected, bool useEqualityRules = false)
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
            stringComparer = (actualValue, expectedValue) => Equality<string>.Default.Equal(actualValue, expectedValue);
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

        return new(actual, expected, string.Join(", ", optionsList.ToArray()), sensitivity, stringComparer);
    }
}
