namespace Shouldly.Tests.ShouldBeNegative;

public class LongScenario
{
    [Fact]
    public void LongScenarioShouldFail()
    {
        var @long = 3L;
        Verify.ShouldFail(() =>
            @long.ShouldBeNegative("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        (-7L).ShouldBeNegative();
    }
}