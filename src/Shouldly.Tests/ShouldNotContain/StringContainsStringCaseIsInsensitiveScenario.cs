namespace Shouldly.Tests.ShouldNotContain;

public class StringContainsStringCaseIsInsensitiveScenario
{
    [Fact]
    public void StringContainsStringCaseIsInsensitiveScenarioShouldFail()
    {
        const string target = "Shouldly is legendary";
        Verify.ShouldFail(() =>
            target.ShouldNotContain("LEGENDARY"));
    }

    [Fact]
    public void ShouldPass()
    {
        "Shouldly is legendary".ShouldNotContain("LEGEND-wait for it-ary");
    }
}