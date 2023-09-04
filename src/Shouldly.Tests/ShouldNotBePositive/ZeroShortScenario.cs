namespace Shouldly.Tests.ShouldNotBePositive;

public class ZeroShortScenario
{
    [Fact]
    public void ZeroShortScenarioShouldPass()
    {
        ((short)0).ShouldNotBePositive();
    }
}