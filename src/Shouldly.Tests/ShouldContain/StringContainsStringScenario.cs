namespace Shouldly.Tests.ShouldContain;

public class StringContainsStringScenario
{
    private const string Target = "Shouldly is legendary";

    [Fact]
    public void StringContainsStringScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                Target.ShouldContain("legend-wait for it-ary"),

            errorWithSource:
            @"Target
    should contain (case insensitive comparison)
""legend-wait for it-ary""
    but was actually
""Shouldly is legendary""",

            errorWithoutSource:
            @"""Shouldly is legendary""
    should contain (case insensitive comparison)
""legend-wait for it-ary""
    but did not");
    }

    [Fact]
    public void ShouldPass()
    {
        Target.ShouldContain("legendary");
    }
}