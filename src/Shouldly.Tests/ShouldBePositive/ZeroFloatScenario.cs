namespace Shouldly.Tests.ShouldBePositive;

public class ZeroFloatScenario
{
    [Fact]
    public void ZeroFloatScenarioShouldFail()
    {
        var val = 0f;
        Verify.ShouldFail(() =>
            val.ShouldBePositive("Some additional context"));
    }
}