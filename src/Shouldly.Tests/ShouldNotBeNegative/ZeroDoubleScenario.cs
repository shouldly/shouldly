namespace Shouldly.Tests.ShouldNotBeNegative;

public class ZeroDoubleScenario
{
    [Fact]
    public void ZeroDoubleScenarioShouldPass()
    {
        0.0.ShouldNotBeNegative();
    }
}