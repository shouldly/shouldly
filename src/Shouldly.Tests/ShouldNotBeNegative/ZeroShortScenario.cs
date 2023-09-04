namespace Shouldly.Tests.ShouldNotBeNegative;

public class ZeroShortScenario
{
    [Fact]
    public void ZeroShortScenarioShouldPass()
    {
        ((short)0).ShouldNotBeNegative();
    }
}