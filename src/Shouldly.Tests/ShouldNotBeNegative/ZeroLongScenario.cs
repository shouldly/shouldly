namespace Shouldly.Tests.ShouldNotBeNegative;

public class ZeroLongScenario
{
    [Fact]
    public void ZeroLongScenarioShouldPass()
    {
        0L.ShouldNotBeNegative();
    }
}