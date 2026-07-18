namespace Shouldly.Tests.ShouldBeGreaterThan;

public class IntScenario
{
    [Fact]
    public void IntScenarioShouldFail()
    {
        var one = 1;
        Verify.ShouldFail(() =>
            one.ShouldBeGreaterThan(7, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        7.ShouldBeGreaterThan(1);
    }
}