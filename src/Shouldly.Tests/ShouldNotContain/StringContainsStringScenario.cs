namespace Shouldly.Tests.ShouldNotContain;

public class StringContainsStringScenario
{
    private const string Target = "Shouldly is legendary";

    [Fact]
    public void StringContainsStringScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            Target.ShouldNotContain("legendary"));
    }

    [Fact]
    public void ShouldPass()
    {
        Target.ShouldNotContain("legend-wait for it-ary");
    }
}