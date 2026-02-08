namespace Shouldly.Tests.ShouldBeNegative;

[UseCulture("en-US")]
public class DoubleScenario
{
    [Fact]
    public void DoubleScenarioShouldFail()
    {
        var @decimal = 3.5;
        Verify.ShouldFail(() =>
            @decimal.ShouldBeNegative("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        (-7.5).ShouldBeNegative();
    }
}