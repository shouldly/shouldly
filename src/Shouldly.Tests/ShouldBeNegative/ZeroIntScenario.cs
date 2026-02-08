namespace Shouldly.Tests.ShouldBeNegative;

public class ZeroIntScenario
{
    [Fact]
    public void ZeroIntScenarioShouldFail()
    {
        var val = 0;
        Verify.ShouldFail(() =>
            val.ShouldBeNegative("Some additional context"));
    }
}