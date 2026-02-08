namespace Shouldly.Tests.Strings.DetailedDifference.CaseSensitive;

public class UnsafeStringSpaceScenario
{
    [Fact]
    public void UnsafeStringSpaceScenarioShouldFail()
    {
        var str = "StringOne Space";
        Verify.ShouldFail(() =>
            str.ShouldBe("Stringone\tSpace"));
    }

    [Fact]
    public void ShouldPass()
    {
        "StringOne Space".ShouldBe("StringOne Space");
    }
}
