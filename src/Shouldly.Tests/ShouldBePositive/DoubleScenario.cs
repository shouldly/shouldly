namespace Shouldly.Tests.ShouldBePositive;

public class DoubleScenario
{
    [Fact]
    public void DoubleScenarioShouldFail()
    {
        var @double = -3.5;
        Verify.ShouldFail(() =>
            @double.ShouldBePositive("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        7.5.ShouldBePositive();
    }
}