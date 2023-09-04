namespace Shouldly.Tests.ShouldNotBePositive;

public class ZeroDoubleScenario
{
    [Fact]
    public void ZeroDoubleScenarioShouldPass()
    {
        0.0.ShouldNotBePositive();
    }
}