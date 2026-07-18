namespace Shouldly.Tests.ShouldBeNegative;

public class ZeroLongScenario
{
    [Fact]
    public void ZeroLongScenarioShouldFail()
    {
        var val = 0L;
        Verify.ShouldFail(() =>
            val.ShouldBeNegative("Some additional context"));
    }
}