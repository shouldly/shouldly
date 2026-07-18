namespace Shouldly.Tests.ShouldBeNegative;

public class ZeroFloatScenario
{
    [Fact]
    public void ZeroFloatScenarioShouldFail()
    {
        var val = 0f;
        Verify.ShouldFail(() =>
            val.ShouldBeNegative("Some additional context"));
    }
}