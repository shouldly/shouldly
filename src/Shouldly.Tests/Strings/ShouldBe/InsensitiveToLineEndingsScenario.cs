namespace Shouldly.Tests.Strings.ShouldBe;

public class InsensitiveToLineEndingsScenario
{
    [Fact]
    public void InsensitiveToLineEndingsScenarioShouldFail()
    {
        var str = "line1\nline2";
        Verify.ShouldFail(() =>
            str.ShouldBe("line1\r\nLine3", "Some additional context", StringCompareShould.IgnoreLineEndings));
    }

    [Fact]
    public void ShouldPass()
    {
        "line1\nline2".ShouldBe("line1\r\nline2", StringCompareShould.IgnoreLineEndings);
    }
}
