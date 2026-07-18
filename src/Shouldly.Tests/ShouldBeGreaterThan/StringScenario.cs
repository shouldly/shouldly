namespace Shouldly.Tests.ShouldBeGreaterThan;

public class StringScenario
{
    [Fact]
    public void StringScenarioShouldFail()
    {
        var aVar = "a";
        Verify.ShouldFail(() =>
            aVar.ShouldBeGreaterThan("b", "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        "b".ShouldBeGreaterThan("a");
    }
}