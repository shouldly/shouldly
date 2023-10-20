namespace Shouldly.Tests.ShouldNotBeNegative;

public class ZeroDecimalScenario
{
    [Fact]
    public void ZeroDecimalScenarioShouldPass()
    {
        0m.ShouldNotBeNegative();
    }
}