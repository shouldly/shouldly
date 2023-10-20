namespace Shouldly.Tests.ShouldNotBeNegative;

public class ZeroFloatScenario
{
    [Fact]
    public void ZeroFloatScenarioShouldPass()
    {
        0f.ShouldNotBeNegative();
    }
}