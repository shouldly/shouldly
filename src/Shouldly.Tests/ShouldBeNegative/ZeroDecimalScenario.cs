namespace Shouldly.Tests.ShouldBeNegative;

public class ZeroDecimalScenario
{
    [Fact]
    public void ZeroDecimalScenarioShouldFail()
    {
        var val = 0m;
        Verify.ShouldFail(() =>
            val.ShouldBeNegative("Some additional context"));
    }
}