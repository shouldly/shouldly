namespace Shouldly.Tests.ShouldBePositive;

public class ZeroLongScenario
{
    [Fact]
    public void ZeroLongScenarioShouldFail()
    {
        var val = 0L;
        Verify.ShouldFail(() =>
            val.ShouldBePositive("Some additional context"));
    }
}