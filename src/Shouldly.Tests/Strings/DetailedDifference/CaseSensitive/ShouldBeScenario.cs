namespace Shouldly.Tests.Strings.DetailedDifference.CaseSensitive;

public class ShouldBeScenario
{
    [Fact]
    public void ShouldBeScenarioShouldFail()
    {
        var str = "Stringone";
        Verify.ShouldFail(() =>
            str.ShouldBe("StringOne"));
    }

    [Fact]
    public void ShouldPass()
    {
        "StringOne".ShouldBe("StringOne");
    }
}