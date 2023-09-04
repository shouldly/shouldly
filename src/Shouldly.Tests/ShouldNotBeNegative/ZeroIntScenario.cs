namespace Shouldly.Tests.ShouldNotBeNegative;

public class ZeroIntScenario
{
    [Fact]
    public void ZeroIntScenarioShouldPass()
    {
        0.ShouldNotBeNegative();
    }
}