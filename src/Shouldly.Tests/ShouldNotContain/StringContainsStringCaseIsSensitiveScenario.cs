namespace Shouldly.Tests.ShouldNotContain;

public class StringContainsStringCaseIsSensitiveScenario
{
    [Fact]
    public void StringContainsStringCaseIsSensitiveScenarioShouldFail()
    {
        const string target = "Shouldly is legendary";
        Verify.ShouldFail(() =>
            target.ShouldNotContain("legendary", Case.Sensitive));
    }

    [Fact]
    public void ShouldPass()
    {
        "Shouldly is legendary".ShouldNotContain("LEGENDARY", Case.Sensitive);
    }
}