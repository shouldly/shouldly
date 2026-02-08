namespace Shouldly.Tests.ShouldBeGreaterOrEqualTo;

public class StringScenario
{
    [Fact]
    public void StringScenarioShouldFail()
    {
        var aVar = "a";
        Verify.ShouldFail(() =>
            aVar.ShouldBeGreaterThanOrEqualTo("b", "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        "a".ShouldBeGreaterThanOrEqualTo("a");
    }
}