namespace Shouldly.Tests.Strings.ShouldBe;

public class CaseIsInsensitiveScenario
{
    [Fact]
    public void CaseIsInsensitiveScenarioShouldFail()
    {
        var str = "SamplE";
        Verify.ShouldFail(() =>
            str.ShouldBe("different", "Some additional context", StringCompareShould.IgnoreCase));
    }

    [Fact]
    public void ShouldPass()
    {
        "SamplE".ShouldBe("sAMPLe", StringCompareShould.IgnoreCase);
    }
}