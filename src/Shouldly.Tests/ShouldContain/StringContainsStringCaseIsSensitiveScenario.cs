namespace Shouldly.Tests.ShouldContain;

public class StringContainsStringCaseIsSensitiveScenario
{
    [Fact]
    public void StringContainsStringCaseIsSensitiveScenarioShouldFail()
    {
        const string target = "Shouldly is LEGENDARY";
        Verify.ShouldFail(() =>
            target.ShouldContain("legendary", Case.Sensitive));
    }

    [Fact]
    public void ShouldPass()
    {
        "Shouldly is LEGENDARY".ShouldContain("LEGENDARY", Case.Sensitive);
    }
}