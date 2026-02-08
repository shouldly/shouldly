namespace Shouldly.Tests.ShouldBeNegative;

public class IntScenario
{
    [Fact]
    public void IntScenarioShouldFail()
    {
        var @int = 3;
        Verify.ShouldFail(() =>
            @int.ShouldBeNegative("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        (-7).ShouldBeNegative();
    }
}