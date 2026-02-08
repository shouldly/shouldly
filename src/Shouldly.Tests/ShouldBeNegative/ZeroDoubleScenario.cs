namespace Shouldly.Tests.ShouldBeNegative;

public class ZeroDoubleScenario
{
    [Fact]
    public void ZeroDoubleScenarioShouldFail()
    {
        var val = 0.0;
        Verify.ShouldFail(() =>
            val.ShouldBeNegative("Some additional context"));
    }
}