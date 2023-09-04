namespace Shouldly.Tests.ShouldNotBePositive;

public class ZeroLongScenario
{
    [Fact]
    public void ZeroLongScenarioShouldPass()
    {
        0L.ShouldNotBePositive();
    }
}