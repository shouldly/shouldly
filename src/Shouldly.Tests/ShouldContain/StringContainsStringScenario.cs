namespace Shouldly.Tests.ShouldContain;

public class StringContainsStringScenario
{
    private const string Target = "Shouldly is legendary";

    [Fact]
    public void StringContainsStringScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            Target.ShouldContain("legend-wait for it-ary"));
    }

    [Fact]
    public void ShouldPass()
    {
        Target.ShouldContain("legendary");
    }
}