namespace Shouldly.Tests.ShouldBePositive;

public class IntScenario
{
    [Fact]
    public void IntScenarioShouldFail()
    {
        var @int = -3;
        Verify.ShouldFail(() =>
            @int.ShouldBePositive("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        7.ShouldBePositive();
    }
}