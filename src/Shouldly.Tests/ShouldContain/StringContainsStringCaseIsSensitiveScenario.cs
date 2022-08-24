namespace Shouldly.Tests.ShouldContain;

public class StringContainsStringCaseIsSensitiveScenario
{
    [Fact]
    public void StringContainsStringCaseIsSensitiveScenarioShouldFail()
    {
        const string target = "Shouldly is LEGENDARY";
        Verify.ShouldFail(() =>
                target.ShouldContain("legendary", Case.Sensitive),

            errorWithSource:
            @"target
    should contain
""legendary""
    but was actually
""Shouldly is LEGENDARY""",

            errorWithoutSource:
            @"""Shouldly is LEGENDARY""
    should contain
""legendary""
    but did not");
    }

    [Fact]
    public void ShouldPass()
    {
        "Shouldly is LEGENDARY".ShouldContain("LEGENDARY", Case.Sensitive);
    }
}