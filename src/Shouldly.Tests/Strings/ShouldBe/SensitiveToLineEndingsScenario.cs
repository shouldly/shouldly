namespace Shouldly.Tests.Strings.ShouldBe;

public class SensitiveToLineEndingsScenario
{
    [Fact]
    public void SensitiveToLineEndingsScenarioShouldFail()
    {
        var str = "line1\nline2";
        Verify.ShouldFail(() =>
            str.ShouldBe("line1\r\nline2", "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        "line1\nline2".ShouldBe("line1\nline2");
    }
}
