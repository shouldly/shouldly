namespace Shouldly.Tests.ShouldContain;

public class StringContainsStringCaseIsInsensitiveScenario
{
    [Fact]
    public void StringContainsStringCaseIsInsensitiveScenarioShouldFail()
    {
        const string target = "Shouldly is legendary";
        Verify.ShouldFail(() =>
            target.ShouldContain("legend-wait for it-ary"));
    }

    [Fact]
    public void ShouldPass()
    {
        "Shouldly is legendary".ShouldContain("LEGENDARY");
    }
}