namespace Shouldly.Tests.Strings.ShouldBe;

public class CaseAndLineEndingInsensitiveScenario
{
    [Fact]
    public void CaseAndLineEndingInsensitiveScenarioShouldFail()
    {
        var str = "line1\nline2";
        Verify.ShouldFail(() =>
            str.ShouldBe("line1\r\nLine3", "Some additional context", StringCompareShould.IgnoreLineEndings | StringCompareShould.IgnoreCase));
    }

    [Fact]
    public void ShouldPass()
    {
        "line1\nline2".ShouldBe("line1\r\nLine2",
            StringCompareShould.IgnoreLineEndings | StringCompareShould.IgnoreCase);
    }
}
