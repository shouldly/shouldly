namespace Shouldly.Tests.Strings.ShouldBe;

public class CaseIsSensitiveScenario
{
    [Fact]
    public void CaseIsSensitiveScenarioShouldFail()
    {
        var str = "SamplE";
        Verify.ShouldFail(() =>
            str.ShouldBe("sAMPLe", "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        "SamplE".ShouldBe("SamplE");
    }
}