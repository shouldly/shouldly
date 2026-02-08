namespace Shouldly.Tests.ShouldBePositive;

public class ZeroDecimalScenario
{
    [Fact]
    public void ZeroDecimalScenarioShouldFail()
    {
        // TODO is zero negative?
        var val = 0m;
        Verify.ShouldFail(() =>
            val.ShouldBePositive("Some additional context"));
    }
}