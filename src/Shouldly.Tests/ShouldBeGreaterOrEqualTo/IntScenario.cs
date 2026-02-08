namespace Shouldly.Tests.ShouldBeGreaterOrEqualTo;

public class IntScenario
{
    [Fact]
    public void IntScenarioShouldFail()
    {
        var one = 1;
        Verify.ShouldFail(() =>
            one.ShouldBeGreaterThanOrEqualTo(7, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        1.ShouldBeGreaterThanOrEqualTo(1);
    }
}