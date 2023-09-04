namespace Shouldly.Tests.ShouldNotBePositive;

public class ZeroDecimalScenario
{
    [Fact]
    public void ZeroDecimalScenarioShouldPass()
    {
        0m.ShouldNotBePositive();
    }
}