namespace Shouldly.Tests.Strings.DetailedDifference.CaseInsensitive;

public class CaseInsensitiveShouldBeScenario
{
    [Fact]
    public void CaseInsensitiveShouldBeScenarioShouldFail()
    {
        var str = "StringOneX";
        Verify.ShouldFail(() =>
            str.ShouldBe("Stringone", StringCompareShould.IgnoreCase));
    }

    [Fact]
    public void ShouldPass()
    {
        "StringOne".ShouldBe("Stringone", StringCompareShould.IgnoreCase);
    }
}