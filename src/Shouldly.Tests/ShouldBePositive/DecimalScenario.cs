namespace Shouldly.Tests.ShouldBePositive;

[UseCulture("en-US")]
public class DecimalScenario
{
    [Fact]
    public void DecimalScenarioShouldFail()
    {
        var @decimal = -3.5m;
        Verify.ShouldFail(() =>
            @decimal.ShouldBePositive("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        7.5m.ShouldBePositive();
    }
}