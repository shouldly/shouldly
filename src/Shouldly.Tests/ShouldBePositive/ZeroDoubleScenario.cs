namespace Shouldly.Tests.ShouldBePositive;

public class ZeroDoubleScenario
{
    [Fact]
    public void ZeroDoubleScenarioShouldFail()
    {
        var val = 0.0;
        Verify.ShouldFail(() =>
            val.ShouldBePositive("Some additional context"));
    }
}