namespace Shouldly.Tests.ShouldBePositive;

public class LongScenario
{
    [Fact]
    public void LongScenarioShouldFail()
    {
        var val = -3L;
        Verify.ShouldFail(() =>
            val.ShouldBePositive("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        7L.ShouldBePositive();
    }
}