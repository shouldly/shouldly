namespace Shouldly.Tests.ShouldNotBePositive;

public class ZeroIntScenario
{
    [Fact]
    public void ZeroIntScenarioShouldPass()
    {
        0.ShouldNotBePositive();
    }
}