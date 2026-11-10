namespace Shouldly.Tests.Strings.DetailedDifference.CaseInsensitive;

public class UnicodeNormalizationScenario
{
    [Fact]
    public void DecomposedUpperCaseVsComposedLowerCaseShouldFail()
    {
        var str = "É";
        Verify.ShouldFail(() =>
            str.ShouldBe("é", StringCompareShould.IgnoreCase));
    }
}
