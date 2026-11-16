namespace Shouldly.Tests.ShouldBeNegative;

public class DecimalScenario
{
    [Fact]
    public void DecimalScenarioShouldFail()
    {
        var @decimal = 3.5m;
        Verify.ShouldFail(() =>
            @decimal.ShouldBeNegative("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        (-7.5m).ShouldBeNegative();
    }
}