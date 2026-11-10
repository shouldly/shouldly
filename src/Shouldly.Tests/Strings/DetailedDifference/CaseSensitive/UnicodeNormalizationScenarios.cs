namespace Shouldly.Tests.Strings.DetailedDifference.CaseSensitive;

public class UnicodeNormalizationScenarios
{
    [Fact]
    public void ExpectedComposedActualDecomposed()
    {
        // e + combining acute accent (U+0301) vs precomposed é (U+00E9)
        var str = "é";
        Verify.ShouldFail(() =>
            str.ShouldBe("é"));
    }

    [Fact]
    public void ExpectedDecomposedActualComposed()
    {
        var str = "é";
        Verify.ShouldFail(() =>
            str.ShouldBe("é"));
    }

    [Fact]
    public void SeveralDifferencesInOneWord()
    {
        var str = "résumé";
        Verify.ShouldFail(() =>
            str.ShouldBe("résumé"));
    }

    [Fact]
    public void NeitherNormalizationForm()
    {
        // ANGSTROM SIGN (U+212B) is a singleton decomposition: it is neither NFC nor NFD,
        // but it is canonically equivalent to Å (U+00C5)
        var str = "Å";
        Verify.ShouldFail(() =>
            str.ShouldBe("Å"));
    }

    [Fact]
    public void MultiLine()
    {
        var str = "first line\ncafé\nlast line";
        Verify.ShouldFail(() =>
            str.ShouldBe("first line\ncafé\nlast line"));
    }

    [Fact]
    public void LongString()
    {
        var prefix = new string('a', 70);
        var str = prefix + "café";
        Verify.ShouldFail(() =>
            str.ShouldBe(prefix + "café"));
    }

    [Fact]
    public void CompatibilityEquivalenceIsNotReportedAsNormalization()
    {
        // The "fi" ligature (U+FB01) only matches "fi" under compatibility (NFKC) normalization,
        // which changes meaning, so it must not be reported as a normalization-only difference
        var ex = Should.Throw<ShouldAssertException>(() => "ﬁ".ShouldBe("fi"));
        ex.Message.ShouldNotContain("normalization");
    }

    [Fact]
    public void InvalidUnicodeDoesNotThrowWhileBuildingTheMessage()
    {
        // A lone surrogate makes string.Normalize throw; the hint must be skipped, not crash
        var str = "\uD800é";
        var ex = Should.Throw<ShouldAssertException>(() => str.ShouldBe("\uD800é"));
        ex.Message.ShouldNotContain("normalization");
    }
}
