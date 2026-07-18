namespace Shouldly.Tests.ShouldBeGreaterThan;

public class StringVsNullScenario
{
    [Fact]
    public void StringVsNullScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            ((string?)null).ShouldBeGreaterThan("b", "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        "b".ShouldBeGreaterThan(null);
    }
}