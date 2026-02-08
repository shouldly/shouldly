namespace Shouldly.Tests.Strings.DetailedDifference.CaseInsensitive;

public class UnsafeStringSpaceScenario
{
    [Fact]
    public void UnsafeStringSpaceScenarioShouldFail()
    {
        var str = "StringOne Space";
        Verify.ShouldFail(() =>
            str.ShouldBe("Stringone\tSpace", StringCompareShould.IgnoreCase));
    }

    [Fact]
    public void ShouldPass()
    {
        "StringOne Space".ShouldBe("Stringone Space", StringCompareShould.IgnoreCase);
    }
}
