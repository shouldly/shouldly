namespace Shouldly.Tests.ShouldNotBePositive;

public class ZeroFloatScenario
{
    [Fact]
    public void ZeroFloatScenarioShouldPass()
    {
        0f.ShouldNotBePositive();
    }
}